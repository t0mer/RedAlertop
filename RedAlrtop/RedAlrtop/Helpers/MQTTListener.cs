using Newtonsoft.Json;
using RedAlrtop.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace RedAlrtop.Helpers
{
    public class MQTTListener
    {
        public event EventHandler<AlertEventArgs> OnAlert;
        private List<Alert> alerts;
        private MqttClient client;
        private string clientId;
        private string MQTTBroker;
        private string MQTTUser;
        private string MQTTPassword;
        private int MQTTPort;
        private string Region;

        public MQTTListener()
        {
            alerts = new List<Alert>();
            clientId = Guid.NewGuid().ToString();
            this.MQTTBroker = ConfigurationManager.AppSettings["MQTTBroker"];
            this.MQTTUser = ConfigurationManager.AppSettings["MQTTUser"];
            this.MQTTPassword = ConfigurationManager.AppSettings["MQTTPass"];
            this.MQTTPort = int.Parse(ConfigurationManager.AppSettings["MQTTPort"]);
            this.Region = ConfigurationManager.AppSettings["Region"];

    }

        public void Connect()
        {
            try
            {
                if (client == null)
                {
                    client = new MqttClient(MQTTBroker);
                    client.MqttMsgPublished += client_MqttMsgPublished;
                    client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                    client.ConnectionClosed += Client_ConnectionClosed;

                    var resule = client.Connect(clientId, MQTTUser, MQTTPassword);
                    ushort msgId = client.Subscribe(new string[] { "/redalert" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

                }
            }
            catch (Exception ex)
            {
                client = null;

            }
        }

        private void TryReconnect()
        {
            var connected = client.IsConnected;
            while (!connected)
            {
                try
                {
                    client.Connect(clientId, MQTTUser, MQTTPassword);
                }
                catch
                {

                }
                connected = client.IsConnected;
                Thread.Sleep(1000);
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (client != null)
                {
                    if (client.IsConnected)
                    {
                        client.Publish("/redalert/LWT", Encoding.ASCII.GetBytes("Disconnected"));
                        client.Unsubscribe(new string[] { "/redalert" });
                        client.MqttMsgPublished -= client_MqttMsgPublished;
                        client.MqttMsgPublishReceived -= Client_MqttMsgPublishReceived;
                        client.ConnectionClosed -= Client_ConnectionClosed;
                        client.Disconnect();


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }




        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ALertData = Encoding.UTF8.GetString(e.Message);
            if (!String.IsNullOrEmpty(ALertData))
            {
                Alert alert = JsonConvert.DeserializeObject<Alert>(ALertData);
                
                //Chec if the specific alert already fired
                bool AlertFired = alerts.Any(item => item.id == alert.id);

                 //if someone registerd to OnAlert Event, rise it
                        if (this.OnAlert != null && !AlertFired)
                        {
                            if (Region == "*" || HasMatch(alert.data.ToArray()))
                            {
                                //Fire the alert
                                AlertEventArgs args = new AlertEventArgs(alert, DateTime.Now);
                                this.OnAlert(this, args);
                                //Add the fired alert to the list of fired alerts in order to avoid multiple alerts
                                alerts.Add(alert);
                                args = null;
                                GC.Collect();
                            }
                        }

            }
        }

        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {

        }

        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            TryReconnect();
        }

        /// <summary>
        /// Check if alert fired for the selected region
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool HasMatch(string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Match match = Regex.Match(Region, data[i], RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return true;
                }
            }
            return false;
        }


    }
}

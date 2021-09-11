using Newtonsoft.Json;
using RedAlrtop.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedAlrtop.Helpers
{
    public class OrefListener
    {
        private System.Timers.Timer timer;
        public event EventHandler<AlertEventArgs> OnAlert;
        private List<Alert> alerts = new List<Alert>();
        private string OrefApiUrl = "https://www.oref.org.il/WarningMessages/alert/alerts.json";
        private string Region = ConfigurationManager.AppSettings["Region"];
        
        /// <summary>
        /// Oref Listenr is running Http request to Oref.org.il in order to recive Red Alert Messages
        /// </summary>
        public OrefListener()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            alerts.Add(new Alert { id = 10000 });

            //init time Instance
            timer = new System.Timers.Timer();
            //Set Timer Tick Interval (In miliseconds)

            //Register to Tick Event
            timer.Elapsed += timer_Tick;
            timer.Interval = 1000;
            timer.Enabled = true;



        }

        public bool Start()
        {
            try
            {

                timer.Start();
                return true;
            }
            catch (Exception ex)
            {
                Logger(ex);
                return false;
            }
        }
        public bool Stop()
        {
            try
            {
                timer.Stop();
                return true;
            }
            catch (Exception ex)
            {
                Logger(ex);
                return false;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            using (var _webClient = new WebClient())
            {
                try
                {

                    //Setting The Http Headers needed to make successful request
                    _webClient.Headers.Add("Referer", "https://www.oref.org.il/");

                    //Using User-Agent Header as Chrome Web Browser ontop of Windows 10
                    _webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.97 Safari/537.36");

                    //Add X-Requested-With Header to the request
                    _webClient.Headers.Add("X-Requested-With", "XMLHttpRequest");

                    //Setting Encoding to UTF-8
                    _webClient.Encoding = Encoding.UTF8;

                    //Get the alert json from Oref website
                    var jsonResult = _webClient.DownloadString(this.OrefApiUrl);

                    //If there is no alert, the json response is Empty so don't fire alert
                    if (!String.IsNullOrEmpty(jsonResult))
                    {
                        //Deserializing Json Result into Alert Object
                        Alert alert = JsonConvert.DeserializeObject<Alert>(jsonResult);

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
                catch (Exception ex)
                {
                    Logger(ex);
                    //In case there is a response error or Exception sleep for 3 seconds and than retry.
                    Thread.Sleep(3000);

                }
            }
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

        private void Logger(Exception ex)
        {

            StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, "Redalert.log"), true, Encoding.UTF8);
            sw.WriteLine(ex.ToString());
            sw.Flush();
            sw.Dispose();
        }


        private void Logger(string info)
        {

            StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, "Redalert.log"), true, Encoding.UTF8);
            sw.WriteLine(info);
            sw.Flush();
            sw.Dispose();
        }


    }
}

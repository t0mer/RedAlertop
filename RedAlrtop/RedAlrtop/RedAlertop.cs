
using Newtonsoft.Json;
using RedAlrtop.Helpers;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;


namespace RedAlrtop
{
    public partial class RedAlertop : Form
    {
        private OrefListener HttpListener;
        private MQTTListener MqttListener;
        StartupManager startupManager = new StartupManager();
        SoundPlayer player = new SoundPlayer();
        private string AlertSource;
        public RedAlertop()
        {


            InitializeComponent();
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SoundFile"]))
            {
                this.SoundFilePath.Text = Path.Combine(Application.StartupPath, "alarmSound.wav");
                SaveSettings();
            }
            else
            {
                this.SoundFilePath.Text = ConfigurationManager.AppSettings["SoundFile"];
                player.SoundLocation = ConfigurationManager.AppSettings["SoundFile"];
            }

        }

        private void RedAlertop_Load(object sender, EventArgs e)
        {
            AutoStart.Checked = startupManager.CheckAutoStart();
            this.SoundFilePath.Text = ConfigurationManager.AppSettings["SoundFile"];
            this.AlertRegion.Text = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["Region"]) ? ConfigurationManager.AppSettings["Region"] : "*";
            this.AlertSource = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["AlertSource"]) ? ConfigurationManager.AppSettings["AlertSource"] : "http";


            if (this.AlertSource == "mqtt")
            {
                this.MqttListener = new MQTTListener();
                MqttListener.OnAlert += Monitor_OnAlert;
                MqttListener.Connect();

            }
            else
            {
                this.HttpListener = new OrefListener();
                HttpListener.OnAlert += Monitor_OnAlert;
                this.HttpListener.Start();
            }
   
            this.HideMe();
            ShowNotification("Red Alert is running", ToolTipIcon.Info);
        }



        private void HideMe()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void ShowMe()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }


        private void RedAlertop_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool stopped = this.AlertSource == "mqtt" ? MqttListener.Disconnect() : HttpListener.Stop();
            notifyIcon.Visible = false;
        }


        private void Monitor_OnAlert(object sender, Common.AlertEventArgs e)
        {
            Logger.AppendLine(Color.White, string.Format("{0} {1} {2} \n", e.AlertDate.ToString("dd/MM/yyyy HH:mm"), e.Alert.title, JsonConvert.SerializeObject(e.Alert.data)));
            string Areas = "";
            foreach (var area in e.Alert.data)
            {
                Areas = Areas + area + " \n";
            }

            Alert(string.Format("{0} \n {1} \n {2}", e.AlertDate.ToString("dd/MM/yyyy HH:mm"), e.Alert.title, Areas));
        }



        private void Save_Click(object sender, EventArgs e)
        {
            SaveSettings();

        }

        private void SaveSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["SoundFile"].Value = this.SoundFilePath.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            if (AutoStart.Checked)
                this.startupManager.EnableAutoStart();
            else
                this.startupManager.DisableAutoStart();



            player.SoundLocation = this.SoundFilePath.Text;
        }

        private void SoundFileBrowser_Click(object sender, EventArgs e)
        {
            DialogResult result = SoundFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(SoundFileDialog.FileName) && result == DialogResult.OK)
            {
                if (File.Exists(SoundFileDialog.FileName))
                    this.SoundFilePath.Text = SoundFileDialog.FileName;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.HideMe();
            else
                this.ShowMe();
        }

        private void ShowNotification(string Message, ToolTipIcon icon = ToolTipIcon.Info)
        {
            this.notifyIcon.ShowBalloonTip(500, "Red Alert", Message, icon);
        }


        private void Alert(string Message)
        {
            this.notifyIcon.ShowBalloonTip(3000, "Red Alert", Message, ToolTipIcon.Warning);
            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }
    }
}

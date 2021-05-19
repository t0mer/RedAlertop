
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
        OrefListener monitor = new OrefListener();
        StartupManager startupManager = new StartupManager();
        SoundPlayer player = new SoundPlayer();
        public RedAlertop()
        {


            InitializeComponent();
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SoundFile"]))
            {
                this.SoundFilePath.Text = Path.Combine(Application.StartupPath, "alarmSound.wav");
                SaveSettings();
            }
            player.SoundLocation = this.SoundFilePath.Text;
        }

        private void RedAlertop_Load(object sender, EventArgs e)
        {
            AutoStart.Checked = startupManager.CheckAutoStart();

            this.SoundFilePath.Text = ConfigurationManager.AppSettings["SoundFile"];
            this.AlertRegion.Text = ConfigurationManager.AppSettings["Region"];
            monitor.OnAlert += Monitor_OnAlert;
            monitor.Start();
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
            monitor.Stop();
            notifyIcon.Visible = false;
        }


        private void Monitor_OnAlert(object sender, Common.AlertEventArgs e)
        {
            Logger.AppendLine(Color.White, string.Format("{0} {1} {2} \n", e.AlertDate.ToString("dd/MM/yyyy HH:mm"), e.Alert.title, JsonConvert.SerializeObject(e.Alert.data)));
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
            player.Play();

        }
    }
}

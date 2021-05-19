using Newtonsoft.Json;
using RedAlrtop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedAlrtop
{
    public partial class RedAlertop : Form
    {
        public RedAlertop()
        {
            InitializeComponent();
        }

        private void RedAlertop_Load(object sender, EventArgs e)
        {
            OrefListener monitor = new OrefListener();
            monitor.OnAlert += Monitor_OnAlert;
            monitor.Start();
        }

        private void Monitor_OnAlert(object sender, Common.AlertEventArgs e)
        {
            string alert = JsonConvert.SerializeObject(e.Alert.data);
            Logger.AppendLine(Color.White,alert);
        }
    }
}

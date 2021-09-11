using Newtonsoft.Json;
using RedAlrtop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedAlrtop
{
    static class Program
    {
        private static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Alert alert = new Alert();
            //alert.title = "התראת צבע אדום";
            //alert.id = 123456;
            //alert.data = new List<string>();
            //alert.data.Add("ניר ישראל");
            //alert.data.Add("אשקלון");

            //string alertstr = JsonConvert.SerializeObject(alert);

            const string appName = "RedAlertop";
            bool createdNew;


            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application  
                return;
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RedAlertop());
        }
    }
}

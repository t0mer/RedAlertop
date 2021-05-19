using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAlrtop.Common
{
    public class AlertEventArgs : EventArgs
    {
        /// <summary>
        /// Return The alert data (id, areas,title)
        /// </summary>
        public Alert Alert { get; private set; }

        /// <summary>
        /// Returns the Alert Date
        /// </summary>
        public DateTime AlertDate { get; private set; }

        /// <summary>
        /// Default instructor to set Event args
        /// </summary>
        /// <param name="alert">Alert Data</param>
        /// <param name="alertDate">Alert Date</param>
        public AlertEventArgs(Alert alert, DateTime alertDate)
        {
            this.Alert = alert;
            this.AlertDate = alertDate;
        }
    }
}

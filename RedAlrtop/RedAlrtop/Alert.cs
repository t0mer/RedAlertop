using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAlrtop
{
    public class Alert
    {
        /// <summary>
        /// list of areas where the alert applies to
        /// </summary>
        public List<string> data { get; set; }
        /// <summary>
        /// Message ID from the alert Message
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Alert Title
        /// </summary>
        public string title { get; set; }



    }
}

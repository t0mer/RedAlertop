using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedAlrtop.Common
{
    public class ExceptionEventArgs : EventArgs
    {
        public Exception exeption { get; set; }

        public string MethodName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace time_delegates
{
    public class TimeEventArgs : EventArgs
    {
        public DateTime Dt { get; private set; }

        public string GetString()
        {
            return Dt.ToString("T");
        }

        public TimeEventArgs()
        {
            Dt = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time_delegates
{
    public static class AlarmArray
    {
        public static List<Alarm> Alarms = new List<Alarm> ();

        public static void Execute(object source, TimeEventArgs e)
        {
            foreach (Alarm alarm in Alarms)
            {
                alarm.Execute(e.Dt.Hour, e.Dt.Minute);
            }
        }
    }
}

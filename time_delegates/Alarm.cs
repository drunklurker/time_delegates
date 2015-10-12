using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time_delegates
{
    public class Alarm
    {
        public readonly DateTime Time;
        public readonly string Description;
        public readonly string id;

        public Alarm(string id, int hours, int minutes, string description)
        {
            if (hours > 23 || hours < 0)
                throw new ArgumentException("часы");
            if (minutes > 59 || minutes < 0)
                throw new ArgumentException("минуты");
            if (String.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");
            this.id = id;
            Time = new DateTime(1, 1, 1, hours, minutes, 0);
            Description = description;
            string filename = String.Format("./id_{0}.txt", id);
            if (!File.Exists(filename))
            {
                StreamWriter f = System.IO.File.CreateText(filename);
                f.WriteLine(String.Format("{0}\n{1}\n{2}\n{3}", id, hours, minutes, description));
                f.Close();
            }
        }

        public override string ToString()
        {
            return String.Format("{0} -- {1}:{2} -- {3}", id, Time.Hour, Time.Minute, Description);
        }

        public void Execute(int hours, int minutes)
        {
            if (hours == Time.Hour && minutes == Time.Minute)
            lock(Program.LockCrutch)
            {
                Console.WriteLine(Description + "\n");
                Console.Beep();
            }
        }
    }
}

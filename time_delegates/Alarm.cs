using System;
using System.IO;

namespace time_delegates
{
    public class Alarm
    {
        public readonly string Description;
        public readonly string id;
        private int _hours, _minutes;
        public int Hours
        {
            get { return _hours; }
            private set
            {
                if (value < 0 || value > 23)
                    throw new ArgumentException("hours");
                _hours = value;
            }
        }

        public int Minutes
        {
            get { return _minutes; }
            private set
            {
                if (value < 0 || value > 60)
                    throw new ArgumentException("minutes");
                _minutes = value;
            }
        }

        public Alarm(string id, int hours, int minutes, string description)
        {
            Hours = hours;
            Minutes = minutes;
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");
            this.id = id;
            Description = description;
            string filename = String.Format("./id_{0}.txt", id);
            if (!File.Exists(filename))
            {
                StreamWriter f = File.CreateText(filename);
                f.WriteLine(String.Format("{0}\n{1}\n{2}\n{3}", id, hours, minutes, description));
                f.Close();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} -- {1}:{2} -- {3}", id, Hours, Minutes, Description);
        }

        public void Execute(int hours, int minutes)
        {
            if (hours == Hours && minutes == Minutes)
            lock(Program.LockCrutch)
            {
                Console.WriteLine(Description + "\n");
                Console.Beep();
            }
        }
    }
}

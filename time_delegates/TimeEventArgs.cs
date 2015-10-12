using System;

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

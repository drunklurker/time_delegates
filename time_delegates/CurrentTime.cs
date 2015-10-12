using System;
using System.IO;
using System.Threading;

namespace time_delegates
{
    public delegate void TimeChanged(object source, TimeEventArgs e);

    class CurrentTime
    {
        public static event TimeChanged SecondChanged;
        public static event TimeChanged MinuteChanged;

        private static int _currentMinute = -1;
        
        public static void MainTime()
        {
            while (Program.KeepOnGoing)
            {
                PushTime();
                Thread.Sleep(1000);
            }
        }

        static CurrentTime()
        {
            SecondChanged += PrintToConsole;
            SecondChanged += PrintToFile;

            MinuteChanged += AlarmArray.Execute;
        }

        private static void PrintToConsole(object source, TimeEventArgs e)
        {
            lock (Program.LockCrutch)
            {
                int column = Console.CursorLeft;
                int row = Console.CursorTop;
                Console.SetCursorPosition(0, 0);

                Console.WriteLine(e.GetString());

                Console.SetCursorPosition(column, row);
            }
        }

        private static void PrintToFile(object source, TimeEventArgs e)
        {
            StreamWriter f = File.CreateText("./CurrentTime.txt");
            f.WriteLine(e.GetString());
            f.Close();
        }

        private static void PushTime()
        {
            DateTime dtNow = DateTime.Now;
            TimeEventArgs e = new TimeEventArgs();
            SecondChanged(null, e);
            if (dtNow.Minute != _currentMinute)
            {
                _currentMinute = dtNow.Minute;
                MinuteChanged(null, e);
            }
        }

    }
}

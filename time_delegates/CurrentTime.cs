using System;
using System.IO;
using System.Threading;

namespace time_delegates
{
    public delegate void MinuteDelegate(int hour, int minute);

    public delegate void SecondDelegate(int hour, int minute, int second);

    class CurrentTime
    {
        public static SecondDelegate SecondChanged;
        public static MinuteDelegate MinuteChanged;

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

        private static void PrintToConsole(int hour, int minute, int second)
        {
            lock (Program.LockCrutch)
            {
                int column = Console.CursorLeft;
                int row = Console.CursorTop;
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);

                Console.SetCursorPosition(column, row);
            }
        }

        private static void PrintToFile(int hour, int minute, int second)
        {
            StreamWriter f = File.CreateText("./CurrentTime.txt");
            f.WriteLine("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            f.Close();
        }

        private static void PushTime()
        {
            DateTime dtNow = DateTime.Now;
            SecondChanged(dtNow.Hour, dtNow.Minute, dtNow.Second);
            if (dtNow.Minute != _currentMinute)
            {
                _currentMinute = dtNow.Minute;
                MinuteChanged(dtNow.Hour, dtNow.Minute);
            }
        }

    }
}

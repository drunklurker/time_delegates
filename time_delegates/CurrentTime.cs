using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace time_delegates
{
    public delegate void TimeChanged(object source, TimeEventArgs e);

    class CurrentTime
    {

        public static event TimeChanged secondChanged;
        public static event TimeChanged minuteChanged;

        private static int _currentMinute = -1;
        
        public static void TopAlignPrint()
        {
            //делаем писалку
            //Print printEverywhere = new Print(PrintToConsole);
            //printEverywhere += WriteToFile;
            //делаем получалку времени

            while (Program.KeepOnGoing)
            {
                PushTime();
                System.Threading.Thread.Sleep(1000);
            }

        }

        static CurrentTime()
        {
            secondChanged += PrintToConsole;
            secondChanged += PrintToFile;

            minuteChanged += AlarmArray.Execute;
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
            StreamWriter f = System.IO.File.CreateText("./CurrentTime.txt");
            f.WriteLine(e.GetString());
            f.Close();
        }

        private static void PushTime()
        {
            DateTime dtNow = DateTime.Now;
            TimeEventArgs e = new TimeEventArgs();
            secondChanged(null, e);
            if (dtNow.Minute != _currentMinute)
            {
                _currentMinute = dtNow.Minute;
                minuteChanged(null, e);
            }
        }

    }
}

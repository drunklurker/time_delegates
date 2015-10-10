using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time_delegates
{
    class CurrentTime
    {
        private delegate void PrintToTarget(string str);

        private delegate void GetTimeParts(ref string collectedTime);

        private static int _currentMinute = -1;
        
        public static void TopAlignPrint()
        {
            //делаем писалку
            //Print printEverywhere = new Print(PrintToConsole);
            //printEverywhere += WriteToFile;
            //делаем получалку времени
            string time = null;
            GetTimeParts getTime = new GetTimeParts(GetHours);
            getTime += GetMinutes;
            getTime += GetSeconds;
            PrintToTarget printEverywhere = new PrintToTarget(PrintToConsole);
            printEverywhere += PrintToFile;

            while (Program.KeepOnGoing)
            {
                GetCurrentTime(getTime, ref time);
                lock (Program.LockCrutch)
                {

                    int column = Console.CursorLeft;
                    int row = Console.CursorTop;
                    Console.SetCursorPosition(0, 0);

                    printEverywhere(time);
                    Console.SetCursorPosition(column, row);
                }

                System.Threading.Thread.Sleep(1000);
            }

        }

        private static void PrintToConsole(string s)
        {
            Console.WriteLine(s);
        }

        private static void PrintToFile(string s)
        {
            StreamWriter f = System.IO.File.CreateText("./CurrentTime.txt");
            f.WriteLine(s);
            f.Close();
        }

        private static void GetCurrentTime(GetTimeParts tp, ref string time)
        {
            tp(ref time);
        }

        private static void GetHours(ref string timeString)
        {
            timeString = DateTime.Now.Hour.ToString("D2");
        }

        private static void GetMinutes(ref string timeString)
        {
            int newMinute = DateTime.Now.Minute;
            if (newMinute != _currentMinute)
            {
                _currentMinute = newMinute;
                lock (Program.Alarms)
                {
                    foreach (Alarm alarm in Program.Alarms)
                    {
                        alarm.Execute(DateTime.Now.Hour, _currentMinute);
                    }
                }
            }
            timeString += String.Format(":{0}", _currentMinute.ToString("D2"));
        }

        private static void GetSeconds(ref string timeString)
        {
            timeString += String.Format(":{0}", DateTime.Now.Second.ToString("D2"));
        }
    }
}

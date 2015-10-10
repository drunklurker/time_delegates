using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace time_delegates
{
    class Program
    {
        public static bool KeepOnGoing = true;
        public static readonly object LockCrutch = new object();
        public static List<Alarm> Alarms = new List<Alarm>();
        static void Main(string[] args)
        {
            LoadAlarms();
            Thread timeThread = new Thread(CurrentTime.TopAlignPrint);
            
            timeThread.Start();
            while (KeepOnGoing)
            {
                Menu.Print();
                int choice;
                while (int.TryParse(Console.ReadLine(), out choice) == false)
                    Console.WriteLine("Неправильный формат ввода, попробуйте ещё раз\n");
                Menu.Choose(choice);
            }
            timeThread.Join();
        }

        public static void LoadAlarms()
        {
            string[] fileNames = System.IO.Directory.GetFiles("./");
            for (int i = 0; i < fileNames.Length; i++)
            {
                string[] name = fileNames[i].Split('/');
                fileNames[i] = name[name.Length - 1];
            }
            foreach (string fileName in fileNames)
            {
                if (fileName.StartsWith("id_"))
                {
                    StreamReader f = new StreamReader(String.Format("./{0}", fileName));
                    Alarms.Add(new Alarm(f.ReadLine(), int.Parse(f.ReadLine()), int.Parse(f.ReadLine()), f.ReadLine()));
                    f.Close();
                }
            }
        }
    }
}

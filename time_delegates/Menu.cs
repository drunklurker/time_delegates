using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time_delegates
{
    class Menu
    {
        private static string _message;
        public static void Print()
        {
            lock (Program.LockCrutch)
            {
                int row = Console.CursorTop;
                for (int i = 5; i < row; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(new string(' ', Console.WindowWidth-1));
                }
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Choose action:");
                Console.WriteLine("1: Create alarm");
                Console.WriteLine("2: Kill alarm");
                Console.WriteLine("0: Exit");
                if (!String.IsNullOrWhiteSpace(_message))
                {
                    Console.WriteLine(_message);
                    _message = null;
                }
            }
        }

        public static void Choose(int choice)
        {
            switch (choice)
            {
                case 1:
                    CreateAlarm();
                    break;
                case 2:
                    DeleteAlarm();
                    break;
                case 0:
                    Program.KeepOnGoing = false;
                    break;
            }
        }

        private static void DeleteAlarm()
        {
            for (int i = 0; i < AlarmArray.Alarms.Count; i++)
            {
                Console.WriteLine("{0} :: {1}", i, AlarmArray.Alarms[i]);
            }
            Console.WriteLine("Which alarm do you want to delete?");
            int deathNumber;
            if (int.TryParse(Console.ReadLine(), out deathNumber) && deathNumber >= 0 &&
                deathNumber < AlarmArray.Alarms.Count)
                lock (AlarmArray.Alarms)
                {
                    string filename = "id_" + Program.Alarms[deathNumber].id + ".txt";
                    File.Delete(filename);
                    AlarmArray.Alarms.RemoveAt(deathNumber);
                }
            else
                lock (Program.LockCrutch)
                {
                    _message = "Alarm Index Error";
                }
        }

        private static void CreateAlarm()
        {
            int hours, minutes;
            Console.WriteLine("Choose time: hours and minutes");
            if (int.TryParse(Console.ReadLine(), out hours) && int.TryParse(Console.ReadLine(), out minutes))
            {
                string id;
                Console.WriteLine("Enter alarm id");
                id = Console.ReadLine();
                string description;
                Console.WriteLine("Enter alarm description:");
                description = Console.ReadLine();
                lock(AlarmArray.Alarms)
                {
                    AlarmArray.Alarms.Add(new Alarm(id, hours, minutes, description));
                }
            }
            else
                _message = "Alarm Error";
        }
    }
}

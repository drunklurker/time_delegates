using System;
using System.Collections.Generic;
using System.IO;

namespace time_delegates
{
    public static class AlarmArray
    {
        private static List<Alarm> _alarmList = new List<Alarm>();

        static AlarmArray()
        {
            LoadAlarms();
        }

        public static void Execute(int hour, int minute)
        {
            foreach (Alarm alarm in _alarmList)
            {
                alarm.Execute(hour, minute);
            }
        }

        public static void LoadAlarms()
        {
            string[] fileNames = Directory.GetFiles("./");
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
                    _alarmList.Add(new Alarm(f.ReadLine(), int.Parse(f.ReadLine()), int.Parse(f.ReadLine()), f.ReadLine()));
                    f.Close();
                }
            }
        }

        public static void DeleteAlarm()
        {
            for (int i = 0; i < _alarmList.Count; i++)
            {
                Console.WriteLine("{0} :: {1}", i, _alarmList[i]);
            }
            Console.WriteLine("Which alarm do you want to delete?");
            int deathNumber;
            if (int.TryParse(Console.ReadLine(), out deathNumber) && deathNumber >= 0 &&
                deathNumber < _alarmList.Count)
                lock (_alarmList)
                {
                    string filename = "id_" + _alarmList[deathNumber].id + ".txt";
                    File.Delete(filename);
                    _alarmList.RemoveAt(deathNumber);
                }
            else
                lock (Program.LockCrutch)
                {
                    Menu.Message = "Alarm Index Error";
                }
        }

        public static void CreateAlarm()
        {
            int hours, minutes;
            Console.WriteLine("Choose time: hours and minutes");
            if (int.TryParse(Console.ReadLine(), out hours) && int.TryParse(Console.ReadLine(), out minutes))
            {
                Console.WriteLine("Enter alarm id");
                string id = Console.ReadLine();
                Console.WriteLine("Enter alarm description:");
                string description = Console.ReadLine();
                lock(_alarmList)
                {
                    _alarmList.Add(new Alarm(id, hours, minutes, description));
                }
            }
            else
                Menu.Message = "Alarm Error";
        }
    }
}

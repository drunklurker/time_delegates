using System;

namespace time_delegates
{
    class Menu
    {
        public static string Message;
        public void Print()
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
                if (!String.IsNullOrWhiteSpace(Message))
                {
                    Console.WriteLine(Message);
                    Message = null;
                }
            }
        }

        public Menu()
        {
        }

        public void Choose(int choice)
        {
            switch (choice)
            {
                case 1:
                    AlarmArray.CreateAlarm();
                    break;
                case 2:
                    AlarmArray.DeleteAlarm();
                    break;
                case 0:
                    Program.KeepOnGoing = false;
                    break;
            }
        }
    }
}

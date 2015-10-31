using System;
using System.Threading;

namespace time_delegates
{
    class Program
    {
        public static bool KeepOnGoing = true;
        public static readonly object LockCrutch = new object();
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Thread timeThread = new Thread(CurrentTime.MainTime);
            
            timeThread.Start();
            while (KeepOnGoing)
            {
                menu.Print();
                int choice;
                while (int.TryParse(Console.ReadLine(), out choice) == false)
                    Console.WriteLine("Неправильный формат ввода, попробуйте ещё раз\n");
                menu.Choose(choice);
            }
            timeThread.Join();
        }
    }
}

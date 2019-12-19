using System;
using System.Diagnostics;

namespace Goodbe
{
    static internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Goodbe";
            Console.SetWindowSize(25, 8);

            //DateTime StartupTime = DateTime.Now - new TimeSpan(0, 0, 0, 0, Environment.TickCount);
            PrintTimes(Process.GetCurrentProcess().StartTime);

            while (true)
            {
                Console.WriteLine("\nStartzeitkorrektur: ");
                string newTime = Console.ReadLine();

                try
                {
                    Console.Clear();
                    PrintTimes(DateTime.Parse(newTime));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void PrintTimes(DateTime startTime)
        {
            Console.WriteLine("Arbeitsbeginn: \t" + startTime.ToShortTimeString());
            Console.WriteLine("6h Pause: \t" + startTime.AddHours(6).ToShortTimeString());
            Console.WriteLine("Feierabend: \t" + startTime.AddHours(7).AddMinutes(36).AddMinutes(30).ToShortTimeString());
            Console.WriteLine("9h Pause: \t" + startTime.AddHours(9).AddMinutes(30).ToShortTimeString());
        }
    }
}
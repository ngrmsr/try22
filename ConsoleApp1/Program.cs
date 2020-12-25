using System;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Timer timer = new Timer(2000);
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();
            //Console.ReadKey();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine("Hi");
                    Task.Delay(2000);
                }

            }, TaskCreationOptions.LongRunning);
            Console.ReadKey();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("hi");
        }
    }
}

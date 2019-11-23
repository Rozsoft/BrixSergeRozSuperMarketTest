using Brix.SergeRoz.SuperMarket.SuperMarketTest;
using System;

namespace Brix.SergeRoz.SuperMarketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start executing Super Market app.");
            Console.WriteLine("Press ESC or Enter to stop");

            Console.WriteLine();

            const int cashiersCount = 5;
           
            ISuperMarket superMarket = new SuperMarket.SuperMarket(cashiersCount) ;
           
            var superMarketProccess = superMarket.StartWork();

            do
            {
                while (!Console.KeyAvailable)
                {
                    // Execute Super Market until Escape.
                }
            } while (Console.ReadKey(true).Key == ConsoleKey.Escape && Console.ReadKey(true).Key != ConsoleKey.Enter);

            
            superMarket.StopWork();

            superMarketProccess.Wait();

            Console.ReadKey();

        }
    }
}

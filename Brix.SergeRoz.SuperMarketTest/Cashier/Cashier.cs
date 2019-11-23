using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Brix.SergeRoz.SuperMarketTest.Customer;

namespace Brix.SergeRoz.SuperMarketTest.Cashier
{
    public class Cashier : ICashier
    {
        public string ID
        {
            get
            {
                return "Cashier:" + Guid.NewGuid().ToString();
            }
        }

        public Cashier()
        {
            Console.WriteLine(this.ID + " Created.");
            Console.WriteLine();
        }

        public async Task ProccessCustomer(ConcurrentQueue<ICustomer> customersQueue, CancellationToken token)
        {
            ICustomer proccessingCustomer;
            bool dequeueSuccesful = false;

            do
            {
                dequeueSuccesful = customersQueue.TryDequeue(out proccessingCustomer);
                if (dequeueSuccesful)
                {
                    Console.WriteLine($"{this.ID} finished serving customer {proccessingCustomer.ID}.");
                    Console.WriteLine();
                }

                await GetProccessDelayTime();
            }

            while (!token.IsCancellationRequested);

        }

        private Task GetProccessDelayTime()
        {
            int delay = new Random().Next(1000, 6000); // Time in milliseconds.
            return Task.Delay(delay);
        }
    }
}

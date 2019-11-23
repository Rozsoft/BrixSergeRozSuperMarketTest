using Brix.SergeRoz.SuperMarket.SuperMarketTest;
using Brix.SergeRoz.SuperMarketTest.Cashier;
using Brix.SergeRoz.SuperMarketTest.Customer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brix.SergeRoz.SuperMarketTest.SuperMarket
{
    public class SuperMarket : ISuperMarket
    {
        private ConcurrentQueue<ICustomer> _customersQueue = new ConcurrentQueue<ICustomer>();
        CancellationTokenSource cts = new CancellationTokenSource();
        
        private bool _isStopWorkRequested = false;

        private ICashier[] _cashiers;

        public SuperMarket(int cashiersCount)
        {
            _cashiers = new ICashier[cashiersCount];
        }

        public async Task StartWork()
        {

            var customersSource = Task.Run(() => CustomersProducer(_customersQueue));

            Task[] processors = new Task[_cashiers.Length];

            for (int i = 0; i < _cashiers.Length; i++)
            {
                processors[i] = Task.Run(() =>
                    new Cashier.Cashier().ProccessCustomer(_customersQueue, cts.Token)
                );
            }

            await customersSource;

            Task.WhenAll(processors).GetAwaiter();

        }

        public void StopWork() 
        {
            _isStopWorkRequested = true;
        }

        async Task CustomersProducer(ConcurrentQueue<ICustomer> customersQueue)
        {
            while(!_isStopWorkRequested)
            {
                await Task.Delay(1000); // one second delay between customers creation

                ICustomer customer = new Customer.Customer();
              
                customersQueue.Enqueue(customer);
            }
        }
    }
}

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
       
        CancellationTokenSource _customerProdCts = new CancellationTokenSource();
        CancellationTokenSource _cashiersCts = new CancellationTokenSource();
        
        CancellationToken _customerCanceletionToken;
        CancellationToken _cashiersCanceletionToken;

        private bool _isStopWorkRequested = false;

        private ICashier[] _cashiers;

        public SuperMarket(int cashiersCount)
        {            
            _cashiers = new ICashier[cashiersCount];

            _customerCanceletionToken = _customerProdCts.Token;
            _cashiersCanceletionToken = _cashiersCts.Token;
        }

        public async Task StartWork()
        {

            var customersSource = Task.Run(() => CustomersProducer(_customersQueue, _customerCanceletionToken));

            Task[] processors = new Task[_cashiers.Length];

            for (int i = 0; i < _cashiers.Length; i++)
            {
                processors[i] = Task.Run(() =>
                    new Cashier.Cashier().ProccessCustomer(_customersQueue, _cashiersCanceletionToken)
                );
            }

            await customersSource;

            Task.WhenAll(processors).GetAwaiter();

        }

        public void StopWork() 
        {
            _customerProdCts.Cancel();
        }

        async Task CustomersProducer(ConcurrentQueue<ICustomer> customersQueue, CancellationToken token)
        {
            do
            {
                await Task.Delay(1000); // one second delay between customers creation

                ICustomer customer = new Customer.Customer();
              
                customersQueue.Enqueue(customer);
            }
            while (!token.IsCancellationRequested) ;
        }
    }
}

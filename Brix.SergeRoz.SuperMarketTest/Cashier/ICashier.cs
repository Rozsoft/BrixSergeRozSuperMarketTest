using Brix.SergeRoz.SuperMarketTest.Customer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brix.SergeRoz.SuperMarketTest.Cashier
{
    public interface ICashier
    {
        string ID { get; }

        Task ProccessCustomer(ConcurrentQueue<ICustomer> customersQueue, CancellationToken token);
    }
}

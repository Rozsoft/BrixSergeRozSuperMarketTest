using Brix.SergeRoz.SuperMarketTest.Cashier;
using Brix.SergeRoz.SuperMarketTest.Customer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Brix.SergeRoz.SuperMarket.SuperMarketTest
{
    public interface ISuperMarket
    {
        public Task StartWork();
        public void StopWork();
    }
}

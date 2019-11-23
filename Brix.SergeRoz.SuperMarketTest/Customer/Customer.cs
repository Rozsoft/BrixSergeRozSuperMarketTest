using System;
using System.Collections.Generic;
using System.Text;

namespace Brix.SergeRoz.SuperMarketTest.Customer
{
    public class Customer : ICustomer
    {
        public string ID { get; }

        public Customer()
        {
            ID = "Customer:" + Guid.NewGuid().ToString();

            Console.WriteLine($"{this.ID} has been created.");
            Console.WriteLine();
        }
    }
}

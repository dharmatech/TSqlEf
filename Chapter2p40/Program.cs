using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter2p40
{
    class Program
    {
        class OrderComparer : IEqualityComparer<Order>
        {
            public bool Equals(Order a, Order b) => 
                a.Empid == b.Empid
                &&
                a.Orderdate.Year == b.Orderdate.Year;

            public int GetHashCode(Order order) => (order.Empid, order.Orderdate.Year).GetHashCode();
        }

        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT DISTINCT empid, YEAR(orderdate) AS orderyear
                // FROM Sales.Orders
                // WHERE custid = 71;                                  

                var result = db.Orders.AsEnumerable()
                    .Where(order => order.Custid == 71)
                    .Distinct(new OrderComparer());

                foreach (var order in result.OrderBy(order => order.Empid))
                {
                    Console.WriteLine("{0} {1}", order.Empid, order.Orderdate.Year);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter2p35
{


    class Program
    {
        class OrderComparer : IEqualityComparer<Order>
        {
            public bool Equals(Order a, Order b) => a.Empid == b.Empid;

            public int GetHashCode(Order order) => order.Empid.GetHashCode();
        }


        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT
                // empid,
                // YEAR(orderdate) AS orderyear,
                // COUNT(DISTINCT custid) AS numcusts
                // FROM Sales.Orders
                // GROUP BY empid, YEAR(orderdate);

                var result = db.Orders.AsEnumerable()
                    .GroupBy(order => new { order.Empid, order.Orderdate.Year });

                foreach (var grouping in result)
                {
                    Console.WriteLine("{0} {1} {2}",
                        grouping.Key.Empid,
                        grouping.Key.Year,
                        grouping.Select(order => order.Custid).Distinct().Count());
                }
            }


            

            using (var db = new TSQLV4Context())
            {
                // SELECT DISTINCT empid, YEAR(orderdate) AS orderyear
                // FROM Sales.Orders
                // WHERE custid = 71;                                  

                var result = db.Orders.AsEnumerable()
                    .Where(order => order.Custid == 71)
                    .Distinct(new OrderComparer());


                foreach (var order in result)
                {
                    Console.WriteLine("{0} {1}", order.Empid, order.Orderdate.Year);
                }
            }


        }
    }
}





using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p139
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT custid, orderid, orderdate, empid
                // FROM Sales.Orders AS O1
                // WHERE orderid =
                //     (
                //         SELECT MAX(Sales.Orders.orderid)
                // 
                //         FROM Sales.Orders
                //         WHERE Sales.Orders.custid = O1.custid
                //     )

                var result = db.Orders.Where(order =>
                    order.Orderid
                    ==
                    db.Orders
                        .Where(ord => ord.Custid == order.Custid)
                        .Select(ord => ord.Orderid)
                        .Max());

                foreach (var order in result)
                {
                    Console.WriteLine("{0,3} {1} {2:yyyy-MM-dd} {3}", order.Custid, order.Orderid, order.Orderdate, order.Empid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

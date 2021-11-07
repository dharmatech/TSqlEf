using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;


namespace Chapter3e4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT C.custid, C.companyname, O.orderid, O.orderdate
                // FROM Sales.Customers AS C
                //   LEFT OUTER JOIN Sales.Orders AS O
                //     ON O.custid = C.custid;

                // Navigation property approach:

                var result = db.Customers.SelectMany(
                    customer => customer.Orders.DefaultIfEmpty(),
                    (customer, order) => new
                    {
                        customer.Custid,
                        customer.Companyname,
                        order
                    });

                foreach (var item in result)
                {
                    Console.WriteLine("{0,3} {1} {2,6} {3,10}",
                        item.Custid,
                        item.Companyname,
                        item.order == null ? "NULL" : item.order.Orderid,
                        item.order == null ? "NULL" : item.order.Orderdate.ToString("yyyy-MM-dd"));
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

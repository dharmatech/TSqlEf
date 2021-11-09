using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TSqlEf;

namespace Chapter4p134
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT orderid, orderdate, empid, custid
                // FROM Sales.Orders
                // WHERE orderid = (SELECT MAX(O.orderid) FROM Sales.Orders AS O);

                var result = db.Orders.Where(order => order.Orderid == db.Orders.Select(order => order.Orderid).Max());

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1:yyyy-MM-dd} {2} {3}", item.Orderid, item.Orderdate, item.Empid, item.Custid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p141b
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT custid, companyname
                // FROM Sales.Customers AS C
                // WHERE country = N'Spain'
                // AND EXISTS
                //   (SELECT * FROM Sales.Orders AS O WHERE O.custid = C.custid);

                var result = db.Customers.Where(customer =>
                    customer.Country == "Spain"
                    &&
                    db.Orders.Any(order => order.Custid == customer.Custid));

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1}", item.Custid, item.Companyname);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

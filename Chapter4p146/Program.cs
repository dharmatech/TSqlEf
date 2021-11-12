using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p146
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // INSERT INTO Sales.Orders
                //   (custid, empid, orderdate, requireddate, shippeddate, shipperid,
                //    freight, shipname, shipaddress, shipcity, shipregion,
                //    shippostalcode, shipcountry)
                //   VALUES(NULL, 1, ‘20160212', ‘20160212',
                //          ‘20160212', 1, 123.00, N'abc', N'abc', N'abc',
                //          N'abc', N'abc', N'abc');

                db.Orders.Add(new Order()
                {
                    Custid = null,
                    Empid = 1,
                    Orderdate = new DateTime(2016, 2, 12),
                    Requireddate = new DateTime(2016, 2, 12),
                    Shippeddate = new DateTime(2016, 2, 12),
                    Shipperid = 1,
                    Freight = 123.00m,
                    Shipname = "abc",
                    Shipaddress = "abc",
                    Shipcity = "abc",
                    Shipregion = "abc",
                    Shippostalcode = "abc",
                    Shipcountry = "abc"
                });

                db.SaveChanges();

                // SELECT custid, companyname
                // FROM Sales.Customers
                // WHERE custid NOT IN(SELECT O.custid FROM Sales.Orders AS O);

                // NULL causes the issue in T-SQL whereas in C#, it's fine.

                var result = db.Customers
                    .Where(customer =>
                        db.Orders
                            .Select(order => order.Custid)
                            .Contains(customer.Custid) == false);

                foreach (var customer in result)
                {
                    Console.WriteLine("{0} {1}", customer.Custid, customer.Companyname);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());

                db.Orders.RemoveRange(db.Orders.Where(order => order.Custid == null));
                db.SaveChanges();
            }




            
        }
    }
}

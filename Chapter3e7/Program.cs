using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter3e7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT C.custid, C.companyname, O.orderid, O.orderdate
                // FROM Sales.Customers AS C
                //   LEFT OUTER JOIN Sales.Orders AS O
                //     ON O.custid = C.custid
                //     AND O.orderdate = '20160212';


                //var result =
                //    from customer in db.Customers
                //    join order in db.Orders
                //    on
                //    customer.Custid equals order.Custid
                //    into Abc
                //    from abc in Abc.DefaultIfEmpty()
                //    select new
                //    {
                //        customer.Custid,
                //        customer.Companyname,
                //        Orderid = abc == null ? -1 : abc.Orderid,
                //        Orderdate = abc == null ? new DateTime() : abc.Orderdate
                //    };

                var result =
                    from customer in db.Customers
                    join order in db.Orders
                    on
                    new
                    {
                        Key1 = customer.Custid,
                        Key2 = true
                    }
                    equals 
                    new
                    {
                        Key1 = order.Custid,
                        Key2 = order.Orderdate == new DateTime(2016, 2, 12)
                    }                    
                    into Abc
                    from abc in Abc.DefaultIfEmpty()
                    select new
                    {
                        customer.Custid,
                        customer.Companyname,
                        Orderid = abc == null ? -1 : abc.Orderid,
                        Orderdate = abc == null ? new DateTime() : abc.Orderdate
                    };

                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Empid);
                //}

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

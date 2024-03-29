﻿using Microsoft.EntityFrameworkCore;
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
                // See this post regarding this query:
                //
                // https://stackoverflow.com/questions/69890853/ef-core-multiple-join-conditions-causing-cs1941

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

                //var result =
                //    from customer in db.Customers
                //    join order in db.Orders
                //    on
                //    new
                //    {
                //        Key1 = customer.Custid,
                //        Key2 = true
                //    }
                //    equals 
                //    new
                //    {
                //        Key1 = order.Custid,
                //        Key2 = order.Orderdate == new DateTime(2016, 2, 12)
                //    }                    
                //    into Abc
                //    from abc in Abc.DefaultIfEmpty()
                //    select new
                //    {
                //        customer.Custid,
                //        customer.Companyname,
                //        Orderid = abc == null ? -1 : abc.Orderid,
                //        Orderdate = abc == null ? new DateTime() : abc.Orderdate
                //    };



                // cly's suggestion to use
                //   where order.Orderdate == new DateTime(2016, 2, 12)
                //
                // Not sure where to insert the where clause. 

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

                //var result =
                //    from customer in db.Customers
                //    join order in db.Orders
                //    on customer.Custid equals order.Custid
                //    into Abc
                //    from abc in Abc.DefaultIfEmpty()
                //    where 
                //    abc.Orderdate == new DateTime(2016, 2, 12)
                //    select new
                //    {
                //        customer.Custid,
                //        customer.Companyname,
                //        Orderid = abc == null ? -1 : abc.Orderid,
                //        Orderdate = abc == null ? new DateTime() : abc.Orderdate
                //    };

                //var result =
                //    from customer in db.Customers
                //    join order in db.Orders
                //    on customer.Custid equals order.Custid
                //    into Abc
                //    from abc in Abc.DefaultIfEmpty()
                //    where
                //    customer.Custid == abc.Custid && abc.Orderdate == new DateTime(2016, 2, 12)
                //    select new
                //    {
                //        customer.Custid,
                //        customer.Companyname,
                //        Orderid = abc == null ? -1 : abc.Orderid,
                //        Orderdate = abc == null ? new DateTime() : abc.Orderdate
                //    };

                // Charlieface suggestion
                //
                // https://stackoverflow.com/a/69892036/268581

                var result =
                    from customer in db.Customers
                    join order in db.Orders
                    on customer.Custid equals order.Custid
                    into Abc
                    from abc in Abc.Where(abc => abc.Orderdate == new DateTime(2016, 2, 12)).DefaultIfEmpty()
                    select new
                    {
                        customer.Custid,
                        customer.Companyname,
                        Orderid = abc == null ? -1 : abc.Orderid,
                        Orderdate = abc == null ? new DateTime() : abc.Orderdate
                    };

                foreach (var item in result)
                {
                    Console.WriteLine("{0,3} {1} {2,6} {3,10}", 
                        item.Custid, 
                        item.Companyname, 
                        item.Orderid == -1 ? "NULL" : item.Orderid, 
                        item.Orderid == -1 ? "NULL" : item.Orderdate.ToString("yyyy-MM-dd"));
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

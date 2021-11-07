using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter3p114
{
    class Program
    {
        static void Main(string[] args)
        {
            // Post about this approach here:
            // 
            // https://stackoverflow.com/q/69869928/268581

            // Another post that discusses LEFT OUTER JOIN in LINQ:
            //
            // https://stackoverflow.com/a/9506274/268581

            using (var db = new TSQLV4Context())
            {
                // SELECT C.custid, C.companyname, O.orderid
                // FROM Sales.Customers AS C
                //   LEFT OUTER JOIN Sales.Orders AS O
                //     ON C.custid = O.custid;

                // Query syntax:

                //var result =
                //    from customer in db.Customers
                //    join order in db.Orders
                //    on customer.Custid equals order.Custid into Abc
                //    from abc in Abc.DefaultIfEmpty()
                //    select new
                //    {
                //        customer.Custid,
                //        customer.Companyname,
                //        orderid = abc == null ? -1 : abc.Orderid
                //    };

                // INNER JOIN (does not return NULL items):

                //var result = db.Customers.Join(
                //    db.Orders,
                //    customer => customer.Custid,
                //    order => order.Custid,
                //    (customer, order) =>
                //        new
                //        {
                //            customer.Custid,
                //            customer.Companyname,
                //            orderid = order.Orderid
                //        }
                //    );

                // GroupJoin / SelectMany approach:

                //var result = db.Customers.GroupJoin(
                //    db.Orders,
                //    customer => customer.Custid,
                //    order => order.Custid,
                //    (customer, orders) => new { customer, orders })
                //    .SelectMany(
                //        customer_orders => customer_orders.orders.DefaultIfEmpty(),
                //        (customer_orders, order) => new
                //        {
                //            customer_orders.customer.Custid,
                //            customer_orders.customer.Companyname,
                //            orderid = order == null? -1 : order.Orderid
                //        });

                // Navigation property approach:

                var result = db.Customers.SelectMany(
                    customer => customer.Orders.DefaultIfEmpty(),
                    (customer, order) => new
                    {
                        customer.Custid,
                        customer.Companyname,
                        orderid = order == null ? -1 : order.Orderid
                    });

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1} {2}", item.Custid, item.Companyname, item.orderid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter3p113
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // Join approach

                var result = db.Customers.Join(
                    db.Orders,
                    customer => customer.Custid,
                    order => order.Custid,
                    (customer, order) => new
                    {
                        customer.Custid,
                        customer.Companyname,
                        order.Orderid
                    })
                    .Join(
                        db.OrderDetails,
                        item => item.Orderid,
                        details => details.Orderid,
                        (item, details) => new
                        {
                            item.Custid,
                            item.Companyname,
                            item.Orderid,
                            details.Productid,
                            details.Qty
                        });

                // Navigation properties approach

                //var result = db.Customers.SelectMany(
                //    customer => customer.Orders,
                //    (customer, order) => new
                //    {
                //        customer.Custid,
                //        customer.Companyname,
                //        order.Orderid,
                //        order.OrderDetails,
                //    })
                //    .SelectMany(
                //        item => item.OrderDetails,
                //        (item, details) => new
                //        {
                //            item.Custid,
                //            item.Companyname,
                //            item.Orderid,
                //            details.Productid,
                //            details.Qty
                //        });

                foreach (var item in result)
                {
                    Console.WriteLine("{0,3} {1} {2} {3,3} {4,3}", 
                        item.Custid, 
                        item.Companyname,
                        item.Orderid,
                        item.Productid,
                        item.Qty);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }


            

        }
    }
}

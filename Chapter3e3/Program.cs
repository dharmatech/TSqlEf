using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter3e3
{
    class Program
    {
        static void Main(string[] args)
        {
            // SELECT C.custid, COUNT(DISTINCT O.orderid) AS numorders, SUM(OD.qty) AS totalqty
            // FROM Sales.Customers AS C
            //   INNER JOIN Sales.Orders AS O
            //     ON O.custid = C.custid
            //   INNER JOIN Sales.OrderDetails AS OD
            //     ON OD.orderid = O.orderid
            // WHERE C.country = N'USA'
            // GROUP BY C.custid;

            using (var db = new TSQLV4Context())
            {
                var result = db.Customers.Join(
                    db.Orders,
                    customer => customer.Custid,
                    order => order.Custid,
                    (customer, order) => new
                    {
                        customer.Custid,
                        customer.Country,
                        order.Orderid
                    })
                    .Join(
                        db.OrderDetails,
                        item => item.Orderid,
                        details => details.Orderid,
                        (item, details) => new
                        {
                            item.Custid,
                            item.Country,
                            item.Orderid,
                            details.Qty
                        })
                    .Where(item => item.Country == "USA")
                    .AsEnumerable()
                    .GroupBy(item => item.Custid)
                    .Select(grouping => new 
                    { 
                        custid = grouping.Key,
                        num_orders = grouping.Select(item => item.Orderid).Distinct().Count(),
                        total_qty = grouping.Select(item => (int)item.Qty).Sum()
                    });

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1,3} {2,4}", item.custid, item.num_orders, item.total_qty);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }





            
        }
    }
}

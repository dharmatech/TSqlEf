using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p138
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // USE TSQLV4;
                // DROP TABLE IF EXISTS dbo.Orders;
                // CREATE TABLE dbo.Orders(orderid INT NOT NULL CONSTRAINT PK_Orders PRIMARY KEY);

                // INSERT INTO dbo.Orders(orderid)
                //   SELECT orderid
                //   FROM Sales.Orders
                //   WHERE orderid % 2 = 0;

                // Instead of creating a temporary table, we setup the following IQueryable:

                var order_ids = db.Orders.Where(order => order.Orderid % 2 == 0).Select(order => order.Orderid);

                // SELECT n
                // FROM dbo.Nums
                // WHERE n BETWEEN (SELECT MIN(O.orderid) FROM dbo.Orders AS O)
                //             AND (SELECT MAX(O.orderid) FROM dbo.Orders AS O)
                //   AND n NOT IN(SELECT O.orderid FROM dbo.Orders AS O);

                var result = db.Nums
                    .Where(num =>
                        num.N >= order_ids.Min() &&
                        num.N <= order_ids.Max() &&
                        order_ids.Contains(num.N) == false);

                foreach (var num in result)
                {
                    Console.WriteLine("{0}", num.N);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }





            
        }
    }
}

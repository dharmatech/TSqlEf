using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p145
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT orderyear, qty,
                //   (SELECT SUM(O2.qty)
                //     FROM Sales.OrderTotalsByYear AS O2
                //     WHERE O2.orderyear <= O1.orderyear) AS runqty
                // FROM Sales.OrderTotalsByYear AS O1
                // ORDER BY orderyear;

                var result = db.OrderTotalsByYears.Select(order1 =>
                new
                {
                    order1.Orderyear,
                    order1.Qty,
                    Runqty = db.OrderTotalsByYears
                        .Where(order2 => order2.Orderyear <= order1.Orderyear)
                        .Select(order2 => order2.Qty)
                        .DefaultIfEmpty()
                        .Sum()
                });

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1,6} {2,6}",
                        item.Orderyear,
                        item.Qty,
                        item.Runqty);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

using System;
using System.Linq;
using TSqlEf;

namespace Chapter2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                {
                    // USE TSQLV4;
                    // SELECT empid, YEAR(orderdate) AS orderyear, COUNT(*) AS numorders
                    // FROM Sales.Orders
                    // WHERE custid = 71
                    // GROUP BY empid, YEAR(orderdate)
                    // HAVING COUNT(*) > 1
                    // ORDER BY empid, orderyear;

                    var result = db.Orders
                        .Where(order => order.Custid == 71)
                        .AsEnumerable()
                        .GroupBy(order => new { order.Empid, order.Orderdate.Year })
                        .Where(grouping => grouping.Count() > 1)
                        .OrderBy(grouping => grouping.Key.Empid)
                        .ThenBy(grouping => grouping.Key.Year)
                        ;

                    foreach (var grouping in result)
                    {
                        Console.WriteLine("{0} {1} {2}",
                            grouping.Key.Empid,
                            grouping.Key.Year,
                            grouping.Count());
                    }
                }

                {
                    // SELECT
                    // empid,
                    // YEAR(orderdate) AS orderyear,
                    // COUNT(DISTINCT custid) AS numcusts
                    // FROM Sales.Orders
                    // GROUP BY empid, YEAR(orderdate);

                    var result = db.Orders.AsEnumerable()
                        .GroupBy(order => new { order.Empid, order.Orderdate.Year });

                    //foreach (var grouping in result)
                    //{
                    //    Console.WriteLine("{0} {1}",
                    //        grouping.Key.Empid,
                    //        grouping.Key.Year
                    //        );

                    //    //foreach (var order in grouping.OrderBy(order => order.Custid))
                    //    //{
                    //    //    Console.WriteLine("  {0}", order.Custid);
                    //    //}


                    //}


                    foreach (var grouping in result)
                    {
                        Console.WriteLine("{0} {1} {2}",
                            grouping.Key.Empid,
                            grouping.Key.Year,
                            grouping.Select(order => order.Custid).Distinct().Count());
                    }




                }




            }
        }
    }
}

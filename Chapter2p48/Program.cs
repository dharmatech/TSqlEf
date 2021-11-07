using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter2p48
{
    class Program
    {
        static void Main(string[] args)
        {
            // Technique used here based on:
            // 
            // https://stackoverflow.com/questions/9980568/row-number-over-partition-by-xxx-in-linq

            using (var db = new TSQLV4Context())
            {
                // SELECT orderid, custid, val,
                //   ROW_NUMBER() OVER(PARTITION BY custid ORDER BY val) AS rownum
                // FROM Sales.OrderValues
                // ORDER BY custid, val;

                var result = db.OrderValues.AsEnumerable()
                    .OrderBy(order_value => order_value.Val)
                    .GroupBy(order_value => order_value.Custid)
                    .Select(grouping => new { grouping, count = grouping.Count() })   
                    .SelectMany(grouping_count =>
                    
                        grouping_count.grouping

                        .Zip(
                            Enumerable.Range(1, grouping_count.count),

                            (order_value, i) => new { order_value.Orderid, order_value.Custid, order_value.Val, i }));

                foreach (var item in result.OrderBy(elt => elt.Custid).ThenBy(elt => elt.Val).Take(20))
                {
                    Console.WriteLine("{0} {1} {2,8} {3}", item.Orderid, item.Custid, item.Val, item.i);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

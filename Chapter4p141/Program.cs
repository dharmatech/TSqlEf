using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p141
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT orderid, custid, val,
                //   CAST(100. * val / (SELECT SUM(O2.val)
                //                        FROM Sales.OrderValues AS O2
                //                        WHERE O2.custid = O1.custid)
                //   AS NUMERIC(5,2)) AS pct
                // FROM Sales.OrderValues AS O1
                // ORDER BY custid, orderid;

                var result =
                    db.OrderValues.Select(order_value => new
                    {
                        order_value.Orderid,
                        order_value.Custid,
                        order_value.Val,
                        Pct = 100.0m * order_value.Val / db.OrderValues.Where(ov => ov.Custid == order_value.Custid).Select(ov => ov.Val).Sum()
                    });

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1,3} {2,9} {3,6}", item.Orderid, item.Custid, item.Val, item.Pct.Value.ToString("#.##"));
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

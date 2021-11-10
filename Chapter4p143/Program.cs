using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p143
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT orderid, orderdate, empid, custid,
                //   (SELECT MAX(O2.orderid)
                //    FROM Sales.Orders AS O2
                //    WHERE O2.orderid < O1.orderid) AS prevorderid
                // FROM Sales.Orders AS O1;

                // Helpful post regarding use of DefaultIfEmpty 
                // 
                // https://stackoverflow.com/questions/38956305/sequence-contains-no-elements-error-max

                var result = db.Orders.Select(order1 =>
                    new
                    {
                        order1.Orderid,
                        order1.Orderdate,
                        order1.Empid,
                        order1.Custid,
                        Prevorderid = db.Orders
                            .Where(order2 => order2.Orderid < order1.Orderid)
                            .Select(order2 => order2.Orderid)
                            .DefaultIfEmpty()
                            .Max()
                    }
                );

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1:yyyy-MM-dd} {2} {3,3} {4}",
                        item.Orderid,
                        item.Orderdate,
                        item.Empid,
                        item.Custid,
                        item.Prevorderid == 0 ? "NULL" : item.Prevorderid
                        );
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

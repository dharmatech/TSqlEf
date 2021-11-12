using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4e1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                var result = db.Orders.Where(order => order.Orderdate == db.Orders.Max(order => order.Orderdate));

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1:yyyy-MM-dd} {2,3} {3}", item.Orderid, item.Orderdate, item.Custid, item.Empid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());

                db.Orders.RemoveRange(db.Orders.Where(order => order.Custid == null));
                db.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace QueryProjectTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                var result = db.Orders.AsEnumerable();

                foreach (var item in result)
                {
                    Console.WriteLine("{0}", item.Empid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

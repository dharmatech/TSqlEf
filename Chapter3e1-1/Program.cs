using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter3e1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT E.empid, E.firstname, E.lastname, N.n
                // FROM HR.Employees AS E
                //   CROSS JOIN dbo.Nums AS N
                // WHERE N.n <= 5
                // ORDER BY n, empid;

                var result = db.Employees.SelectMany(employee =>
                    db.Nums.Select(num =>
                        new
                        {
                            employee.Empid,
                            employee.Firstname,
                            employee.Lastname,
                            num.N
                        }))
                    .Where(item => item.N <= 5);

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1,-10} {2,-10} {3}", item.Empid, item.Firstname, item.Lastname, item.N);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

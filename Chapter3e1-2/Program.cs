using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;


namespace Chapter3e1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT E.empid,
                // DATEADD(day, D.n - 1, CAST('20160612' AS DATE)) AS dt
                // FROM HR.Employees AS E
                // CROSS JOIN dbo.Nums AS D
                // WHERE D.n <= DATEDIFF(day, '20160612', '20160616') + 1
                // ORDER BY empid, dt;

                var result = db.Employees.SelectMany(employee =>
                    db.Nums.Select(num => new { employee.Empid, num.N }))
                    .Where(item => item.N <= new DateTime(2016, 6, 16).Subtract(new DateTime(2016, 6, 12)).Days + 1)
                    .Select(item =>
                        new
                        {
                            item.Empid,
                            dt = new DateTime(2016, 6, 12).Add(new TimeSpan(item.N - 1, 0, 0, 0))
                        })
                    .AsEnumerable()
                    .OrderBy(item => item.Empid)
                    .ThenBy(item => item.dt);

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1:yyyy-MM-dd}", item.Empid, item.dt);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

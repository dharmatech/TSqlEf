using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter4p136
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT orderid
                // FROM Sales.Orders
                // WHERE empid IN
                //   (SELECT E.empid
                //    FROM HR.Employees AS E
                //    WHERE E.lastname LIKE N'D%');

                // This version results in a runtime exception.

                //var result = db.Orders.Where(order =>
                //    db.Employees
                //        .Where(employee => new Regex("^D").IsMatch(employee.Lastname))
                //        .Select(employee => employee.Empid)
                //        .Contains(order.Empid));

                // Factor out the subquery. This version works.

                //var empids = db.Employees.AsEnumerable()
                //    .Where(employee => new Regex("^D").IsMatch(employee.Lastname)).ToList()
                //    .Select(employee => employee.Empid);

                //var result = db.Orders.Where(order => empids.Contains(order.Empid));

                // Alternatively, instead of Regex, use StartsWith:

                var result = db.Orders.Where(order =>
                    db.Employees
                        .Where(employee => employee.Lastname.StartsWith("D"))
                        .Select(employee => employee.Empid)
                        .Contains(order.Empid));

                foreach (var item in result)
                {
                    Console.WriteLine("{0}", item.Orderid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }
        }
    }
}

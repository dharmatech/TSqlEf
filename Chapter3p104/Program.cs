using System;
using System.Collections.Generic;
using System.Linq;
using TSqlEf;

namespace Chapter3p104
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TSQLV4Context())
            {
                // SELECT E.empid, E.firstname, E.lastname, O.orderid
                // FROM HR.Employees AS E
                //   INNER JOIN Sales.Orders AS O
                //     ON E.empid = O.empid;

                var result = db.Employees.Join(
                    db.Orders,
                    employee => employee.Empid,
                    order => order.Empid,
                    (employee, order) => new { employee.Empid, employee.Firstname, employee.Lastname, order.Orderid });

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1} {2} {3}", item.Empid, item.Firstname, item.Lastname, item.Orderid);
                }

                Console.WriteLine();
                Console.WriteLine("{0} rows", result.Count());
            }            
        }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace TSqlEf
{
    public partial class Employee
    {
        public Employee()
        {
            InverseMgr = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        public int Empid { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Title { get; set; }
        public string Titleofcourtesy { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Hiredate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int? Mgrid { get; set; }

        public virtual Employee Mgr { get; set; }
        public virtual ICollection<Employee> InverseMgr { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

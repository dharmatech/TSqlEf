using System;
using System.Collections.Generic;

#nullable disable

namespace TSqlEf
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int Shipperid { get; set; }
        public string Companyname { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

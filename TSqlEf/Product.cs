using System;
using System.Collections.Generic;

#nullable disable

namespace TSqlEf
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Productid { get; set; }
        public string Productname { get; set; }
        public int Supplierid { get; set; }
        public int Categoryid { get; set; }
        public decimal Unitprice { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

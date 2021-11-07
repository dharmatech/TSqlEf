using System;
using System.Collections.Generic;

#nullable disable

namespace TSqlEf
{
    public partial class OrderValue
    {
        public int Orderid { get; set; }
        public int? Custid { get; set; }
        public int Empid { get; set; }
        public int Shipperid { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime Requireddate { get; set; }
        public DateTime? Shippeddate { get; set; }
        public int? Qty { get; set; }
        public decimal? Val { get; set; }
    }
}

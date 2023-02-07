using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace KalbeShop.DataModels
{
    public partial class Penjualan
    {
        public int IntSalesOrderId { get; set; }
        public int? IntCustomerId { get; set; }
        public int? IntProductId { get; set; }
        public DateTime? DtSalesOrder { get; set; }
        public decimal? IntQty { get; set; }
    }
}

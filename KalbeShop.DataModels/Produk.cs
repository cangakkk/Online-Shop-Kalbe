using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace KalbeShop.DataModels
{
    public partial class Produk
    {
        public int IntProductId { get; set; }
        public string TxtProductCode { get; set; }
        public string TxtProductName { get; set; }
        public int? IntQty { get; set; }
        public decimal? DecPrice { get; set; }
        public DateTime? DtInserted { get; set; }
    }
}

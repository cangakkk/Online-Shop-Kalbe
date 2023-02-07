using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KalbeShop.ViewModels
{
    public class VMProduk
    {
        [Column("intProductID")]
        public int IntProductId { get; set; }
        [Required]
        [Column("txtProductCode")]
        [StringLength(5)]
        public string TxtProductCode { get; set; }
        [Column("txtProductName")]
        [StringLength(100)]
        public string TxtProductName { get; set; }
        [Column("intQty")]
        public int? IntQty { get; set; }
        [Column("decPrice", TypeName = "decimal(18, 0)")]
        public decimal? DecPrice { get; set; }
        [Column("dtInserted", TypeName = "datetime")]
        public DateTime? DtInserted { get; set; }
    }
}

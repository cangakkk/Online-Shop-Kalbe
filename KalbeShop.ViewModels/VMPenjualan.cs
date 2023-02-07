using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KalbeShop.ViewModels
{
    public class VMPenjualan
    {
        [Column("intSalesOrderID")]
        public int IntSalesOrderId { get; set; }
        [Column("intCustomerID")]
        public int? IntCustomerId { get; set; }
        [Column("intProductID")]
        public int? IntProductId { get; set; }
        [Column("dtSalesOrder", TypeName = "datetime")]
        public DateTime? DtSalesOrder { get; set; }
        [Column("intQty", TypeName = "decimal(10, 0)")]
        public decimal? IntQty { get; set; }
        [Column("txtCustomerName")]
        [StringLength(100)]
        public string TxtCustomerName { get; set; }
        [Column("txtProductName")]
        [StringLength(100)]
        public string TxtProductName { get; set; }

    }
}

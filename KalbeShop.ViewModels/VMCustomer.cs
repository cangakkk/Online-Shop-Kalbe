using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KalbeShop.ViewModels
{
    public class VMCustomer
    {
        [Column("intCustomerID")]
        public int IntCustomerId { get; set; }
        [Column("txtCustomerName")]
        [StringLength(100)]
        public string TxtCustomerName { get; set; }
        [Column("txtCustomerAddress")]
        [StringLength(100)]
        public string TxtCustomerAddress { get; set; }
        [Column("bitGender")]
        public bool? BitGender { get; set; }
        [Column("dtmBirthDate", TypeName = "datetime")]
        public DateTime? DtmBirthDate { get; set; }
        [Column("dtInserted", TypeName = "datetime")]
        public DateTime? DtInserted { get; set; }
    }
}

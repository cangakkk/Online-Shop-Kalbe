using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace KalbeShop.DataModels
{
    public partial class Customer
    {
        public int IntCustomerId { get; set; }
        public string TxtCustomerName { get; set; }
        public string TxtCustomerAddress { get; set; }
        public bool? BitGender { get; set; }
        public DateTime? DtmBirthDate { get; set; }
        public DateTime? DtInserted { get; set; }
    }
}

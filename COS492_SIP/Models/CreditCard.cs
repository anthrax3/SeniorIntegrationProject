using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COS492_SIP.Models
{
    public class CreditCard
    {
        public string userName { get; set; }
        public string creditCardNumber { get; set; }
        public DateTime expirationDate { get; set; }
        public string svn;
        public string brand;
    }
}
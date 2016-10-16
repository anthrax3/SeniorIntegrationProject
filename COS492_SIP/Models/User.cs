using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COS492_SIP.Models
{
    public class User
    {
        [Key]
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
    }
}

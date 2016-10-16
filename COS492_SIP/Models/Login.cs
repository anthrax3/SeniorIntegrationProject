using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COS492_SIP.Models
{
    public class Login
    {
        [Key]
        public string userName { get; set; }
        public string password { get; set; }
    }
}

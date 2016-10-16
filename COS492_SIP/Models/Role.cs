using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COS492_SIP.Models
{
    public class Role
    {
        [Key]
        public string role { get; set; }
    }
}

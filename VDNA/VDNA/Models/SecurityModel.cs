using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VDNA.Models
{
    public class SecuritySettings
    {
        [Key]
        public string SecurityLevel { get; set; }
    }
}
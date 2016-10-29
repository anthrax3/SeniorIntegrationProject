﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VDNA.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        [StringLength(128)]
        public string UserName { get; set; }

        [MinLength(16), MaxLength(16)]
        [Column(TypeName = "CHAR")]
        public string CardNumber { get; set; }


        [Column(TypeName = "DATE")]
        public DateTime ExpirationDate { get; set; }

        [MinLength(3), MaxLength(3)]
        [Column(TypeName = "CHAR")]
        public string CVV { get; set; }
    }
}
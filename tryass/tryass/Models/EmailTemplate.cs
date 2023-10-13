using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    public class EmailTemplate
    {
        [Key]
        public string Name { get; set; } 
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
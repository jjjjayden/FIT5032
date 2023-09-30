using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    public class Annotation
    {
        public int Id { get; set; }
        public string Comment { get; set; }  // Doctor's comment
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int XrayImageId { get; set; }  // ForeignKey to XrayImage
        public virtual XrayImage XrayImage { get; set; }  // Navigation property
    }
}
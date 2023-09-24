using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    public class XrayImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }  // Path to the stored image
        public DateTime UploadDate { get; set; }
        public int UserId { get; set; }  // ForeignKey to User
        public virtual ApplicationUser User { get; set; }  // Navigation property
        public virtual ICollection<Annotation> Annotations { get; set; } // Related annotations
    }

}
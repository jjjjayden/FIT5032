using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    public class Annotation
    {
        public int Id { get; set; }
        public string Comment { get; set; }  // Doctor's comment
        public int DoctorId { get; set; }   // ForeignKey to User (who is a doctor)
        public virtual ApplicationUser Doctor { get; set; } // Navigation property
        public int XrayImageId { get; set; }  // ForeignKey to XrayImage
        public virtual XrayImage XrayImage { get; set; }  // Navigation property
    }
}
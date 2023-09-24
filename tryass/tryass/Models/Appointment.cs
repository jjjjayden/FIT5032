using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MapId { get; set; }

        [ForeignKey("MapId")]
        public Map Map { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public bool IsBooked { get; set; } = false;

        // Assume you're using ASP.NET Identity, then the user ID is usually a string.
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } // Link to the user who booked the appointment.
    }

}
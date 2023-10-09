using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    public class MapRating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int MapId { get; set; }
        public virtual Map Map { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tryass.Models
{
    [Table("Map")]
    public class Map
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        [Range(-90, 90)] 
        public decimal Latitude { get; set; }

        [Required]
        [Range(-180, 180)] 
        public decimal Longitude { get; set; }


    }

}
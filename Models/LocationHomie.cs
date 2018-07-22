using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HomiesAPI.Models 
{
    public class LocationHomie 
    {
        public int HomieId { get; set; }

        [Required(ErrorMessage = "LocationHomie must link to a Homie")]
        public Homie Homie { get; set; }

        public int LocationId { get; set; }

        [Required(ErrorMessage = "LocationHomie must link to a Location")]
        public Location Location { get; set; }
    }
}
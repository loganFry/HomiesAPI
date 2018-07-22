using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HomiesAPI.Models 
{
    public class LocationHomie 
    {
        public int HomieId { get; set; }

        public Homie Homie { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
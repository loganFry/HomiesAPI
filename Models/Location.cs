using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HomiesAPI.Models 
{
    public class Location 
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Location must have a street name")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Location must have a house number")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Location must have an area code")]
        public string AreaCode { get; set; }

        [Required(ErrorMessage = "Location must have a city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Location must have a state")]
        public string State { get; set; }

        public string DisplayName { get; set; }

        public ICollection<Homie> Homies { get; set; }
    }
}
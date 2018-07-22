using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HomiesAPI.Models 
{
    public class Location 
    {
        public int Id { get;set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string AreaCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public ICollection<LocationHomie> LocationHomies { get; set; }
    }
}
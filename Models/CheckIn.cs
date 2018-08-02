using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using HomiesAPI.DataAccess.Repositories;

namespace HomiesAPI.Models 
{
    public class CheckIn : EntityBase
    {
        [JsonIgnore]
        public Homie Homie { get; set; }
        
        public int HomieId { get; set; }

        public bool WithGuest { get; set; }

        [Required(ErrorMessage = "Checkin must have a time")]
        public DateTime Time { get; set; }
    }
}
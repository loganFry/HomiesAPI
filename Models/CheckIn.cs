using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HomiesAPI.Models 
{
    public class CheckIn 
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Checkin must have an owner")]
        [JsonIgnore]
        public Homie Homie { get; set; }
        
        public int HomieId { get; set; }

        public bool WithGuest { get; set; }

        [Required(ErrorMessage = "Checkin must have a time")]
        public DateTime Time { get; set; }
    }
}
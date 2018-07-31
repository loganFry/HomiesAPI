using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HomiesAPI.Models 
{
    public class CheckOut 
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Checkout must have an owner")]
        [JsonIgnore]
        public Homie Homie { get; set; }

        public int HomieId { get; set; }

        [Required(ErrorMessage = "Checkout must have a time")]
        public DateTime Time { get; set; }
    }
}
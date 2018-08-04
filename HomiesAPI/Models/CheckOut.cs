using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using HomiesAPI.DataAccess.Repositories;

namespace HomiesAPI.Models 
{
    public class CheckOut : EntityBase
    {
        [JsonIgnore]
        public Homie Homie { get; set; }

        public int HomieId { get; set; }

        [Required(ErrorMessage = "Checkout must have a time")]
        public DateTime Time { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace HomiesAPI.Models 
{
    public class CheckOut 
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Checkout must have an owner")]
        public Homie Homie { get; set; }

        [Required(ErrorMessage = "Checkout must have a time")]
        public DateTime Time { get; set; }
    }
}
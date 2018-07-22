using System;
using System.ComponentModel.DataAnnotations;

namespace HomiesAPI.Models 
{
    public class CheckIn 
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Checkin must have an owner")]
        public Homie Homie { get; set; }

        public bool WithGuest { get; set; }

        [Required(ErrorMessage = "Checkin must have a time")]
        public DateTime Time { get; set; }
    }
}
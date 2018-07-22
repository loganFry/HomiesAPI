using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomiesAPI.Models 
{
    public class Homie 
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Homie must have a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Homie must have a last name")]
        public string LastName { get; set; }

        public string NickName { get; set; }

        [Required(ErrorMessage = "Homie must have an email")]
        public string Email { get; set; }

        public bool IsHome { get; set; }

        public bool HasGuest { get; set; }

        public ICollection<CheckIn> CheckIns { get; set; }

        public ICollection<CheckOut> CheckOuts { get; set; }

        public ICollection<LocationHomie> LocationHomies { get; set; }
    }
}
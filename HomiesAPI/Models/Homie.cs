using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomiesAPI.DataAccess.Repositories;

namespace HomiesAPI.Models 
{
    public class Homie : EntityBase
    {
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

        public Location Location { get; set; }

        public int? LocationId { get; set; }
    }
}
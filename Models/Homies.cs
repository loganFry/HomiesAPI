using System;
using System.Collections.Generic;

namespace HomiesAPI.Models
{
    public partial class Homies
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}

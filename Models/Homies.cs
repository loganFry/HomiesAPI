using System;
using System.Collections.Generic;

namespace HomiesAPI.Models
{
    public partial class Homies
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public long Id { get; set; }
    }
}

using HomiesAPI.Models;
using System.Collections.Generic;

namespace HomiesAPI.Tests.Controllers.Homies
{
    public static class HomiesTestHelpers
    {
        
        public static List<Homie> GetHomies()
        {
            var homies = new List<Homie>
            {
                CreateHomie(1),
                CreateHomie(2)
            };

            return homies;
        }

        public static Homie CreateHomie(int id = 1)
        {
            var homie = new Homie
            {
                Id = id,
                FirstName = "Logan",
                LastName = "Fry",
                NickName = "FrenchFry",
                Email = "fry@gmail.com",
                IsHome = false,
                HasGuest = false,
            };

            return homie;
        }
    }
}
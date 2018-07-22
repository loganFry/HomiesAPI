using HomiesAPI.Models;
using System;
using System.Linq;

namespace HomiesAPI.DataAccess
{
    public class DbSeeder
    {
        private HomiesContext _context;


        public DbSeeder(HomiesContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Homies.Any())
            {
                return;
            }

            _context.AddRange(CreateHomies(10));
            _context.SaveChanges();
        }

        public Homie[] CreateHomies(int count)
        {
            Homie[] tempHomies = new Homie[count];
            for(int i = 0; i < count; i++)
            {
                tempHomies[i] = CreateRandomHomie();
            }

            return tempHomies;
        }

        public Homie CreateRandomHomie()
        {
            var isHome = SeedDefaults.GetRandomBool();
            var firstName = SeedDefaults.FirstNames[SeedDefaults.GetRandomInt()];
            
            Homie temp = new Homie 
            {
                FirstName = firstName,
                LastName = SeedDefaults.LastNames[SeedDefaults.GetRandomInt()],
                Email = SeedDefaults.Emails[SeedDefaults.GetRandomInt()],
                IsHome = isHome,
                // HasGuest can only be true if the Homie is home, otherwise
                // it will always be false
                HasGuest = isHome ? SeedDefaults.GetRandomBool() : false,
                NickName = firstName.ToLower() + SeedDefaults.GetRandomInt(100) 
            };

            return temp;
        }
    }
}
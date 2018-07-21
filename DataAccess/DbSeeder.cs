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
            if(_context.Homies.Any())
            {
                return;
            }

            _context.AddRange(CreateHomies());
            _context.SaveChanges();
        }

        public Homies[] CreateHomies()
        {
            Homies[] homies = new Homies[]
            {
                new Homies { FirstName = "Logan", LastName = "Fry", Nickname = "frenchfry", Email = "loganfrybball@yahoo.com" },
                new Homies { FirstName = "Jack", LastName = "Rodman", Nickname = "rod", Email = "rodman.123@osu.edu" },
                new Homies { FirstName = "Kurt", LastName = "Atwell", Nickname = "katwell", Email = "atwell.234@osu.edu" }
            };

            return homies;
        }
    }
}
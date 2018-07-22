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

        public Homie[] CreateHomies()
        {
            Homie[] homies = new Homie[]
            {
                new Homie { FirstName = "Logan", LastName = "Fry", Email = "loganfrybball@yahoo.com" },
                new Homie { FirstName = "Jack", LastName = "Rodman", Email = "rodman.123@osu.edu" },
                new Homie { FirstName = "Kurt", LastName = "Atwell", Email = "atwell.234@osu.edu" }
            };

            return homies;
        }
    }
}
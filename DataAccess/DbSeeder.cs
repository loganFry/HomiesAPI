using HomiesAPI.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

            var homiesNewlyCreated = CreateModels();
            if (homiesNewlyCreated)
            {
                PopulateHomieLinks();
            }
        }

        public bool CreateModels()
        {
            bool homiesNewlyCreated = false;

            if (!_context.Homies.Any())
            {
                _context.AddRange(CreateRandomHomies(10));
                homiesNewlyCreated = true;
            }

            if (!_context.Locations.Any())
            {
                _context.Locations.AddRange(CreateRandomLocations(5));
            }

            _context.SaveChanges();
            return homiesNewlyCreated;
        }

        public void PopulateHomieLinks()
        {
            foreach (var homie in _context.Homies
                .Include(x => x.CheckIns)
                .Include(x => x.CheckOuts)
                .Include(x => x.Location))
            {
                if (homie.CheckIns.Count == 0)
                {
                    homie.CheckIns = CreateRandomCheckIns(homie, 5);
                }

                if (homie.CheckOuts.Count == 0)
                {
                    homie.CheckOuts = CreateRandomCheckOuts(homie, 5);
                }

                if (homie?.Location == null)
                {
                    int maxId = _context.Locations.Max(x => x.Id);
                    int minId = _context.Locations.Min(x => x.Id);
                    homie.Location = _context.Locations
                        .First(x => x.Id == SeedDefaults.GetRandomInt(minId, maxId + 1));
                }
            }

            _context.SaveChanges();
        }

        public Location[] CreateRandomLocations(int count)
        {
            var tempLocations = new Location[count];
            for (int i = 0; i < count; i++)
            {
                tempLocations[i] = CreateRandomLocation();
            }

            return tempLocations;
        }

        public Location CreateRandomLocation()
        {
            var street = SeedDefaults.Streets[SeedDefaults.GetRandomInt()];
            var temp = new Location
            {
                Street = street,
                HouseNumber = SeedDefaults.GetRandomDigitString(),
                AreaCode = SeedDefaults.GetRandomDigitString(),
                City = SeedDefaults.Streets[SeedDefaults.GetRandomInt()],
                State = SeedDefaults.States[SeedDefaults.GetRandomInt()],
                DisplayName = street + SeedDefaults.GetRandomInt(max: 1000)
            };

            return temp;
        }

        public Homie[] CreateRandomHomies(int count)
        {
            var tempHomies = new Homie[count];
            for (int i = 0; i < count; i++)
            {
                tempHomies[i] = CreateRandomHomie();
            }

            return tempHomies;
        }

        public Homie CreateRandomHomie()
        {
            var isHome = SeedDefaults.GetRandomBool();
            var firstName = SeedDefaults.FirstNames[SeedDefaults.GetRandomInt()];

            var temp = new Homie
            {
                FirstName = firstName,
                LastName = SeedDefaults.LastNames[SeedDefaults.GetRandomInt()],
                Email = SeedDefaults.Emails[SeedDefaults.GetRandomInt()],
                IsHome = isHome,
                // HasGuest can only be true if the Homie is home, otherwise
                // it will always be false
                HasGuest = isHome ? SeedDefaults.GetRandomBool() : false,
                NickName = firstName.ToLower() + SeedDefaults.GetRandomInt(max:100)
            };

            return temp;
        }

        public CheckIn[] CreateRandomCheckIns(Homie owner, int count)
        {
            var tempCheckIns = new CheckIn[count];
            for (int i = 0; i < count; i++)
            {
                tempCheckIns[i] = CreateRandomCheckIn(owner);
            }

            return tempCheckIns;
        }

        public CheckIn CreateRandomCheckIn(Homie owner)
        {
            var temp = new CheckIn
            {
                WithGuest = SeedDefaults.GetRandomBool(),
                Homie = owner,
                Time = SeedDefaults.GetRandomDateTime()
            };

            return temp;
        }

        public CheckOut[] CreateRandomCheckOuts(Homie owner, int count)
        {
            var tempCheckIns = new CheckOut[count];
            for (int i = 0; i < count; i++)
            {
                tempCheckIns[i] = CreateRandomCheckOut(owner);
            }

            return tempCheckIns;
        }

        public CheckOut CreateRandomCheckOut(Homie owner)
        {
            var temp = new CheckOut
            {
                Homie = owner,
                Time = SeedDefaults.GetRandomDateTime()
            };

            return temp;
        }
    }
}
using HomiesAPI.Models;
using System;

namespace HomiesAPI.DataAccess.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(HomiesContext context) : base(context)
        {
            
        }

        public void AddHomie(Location location, Homie homieToAdd)
        {
            var locationHomies = _dbContext.Entry(location).Collection(x => x.Homies);
            if(!locationHomies.IsLoaded)
            {
                locationHomies.Load();
            }

            location.Homies.Add(homieToAdd);
            Edit(location);
        }
    }
}
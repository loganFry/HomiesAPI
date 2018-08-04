using HomiesAPI.Models;
using System;

namespace HomiesAPI.DataAccess.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
         void AddHomie(Location location, Homie homieToAdd);
    }
}
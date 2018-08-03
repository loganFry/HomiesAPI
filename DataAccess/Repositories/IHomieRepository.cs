using HomiesAPI.Models;
using System;

namespace HomiesAPI.DataAccess.Repositories
{
    public interface IHomieRepository : IRepository<Homie>
    {
         void CheckIn(Homie homie, bool withGuest, DateTime time);

         void CheckOut(Homie homie, DateTime time);
    }
}
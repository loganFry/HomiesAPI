using HomiesAPI.Models;
using System;

namespace HomiesAPI.DataAccess.Repositories
{
    public class HomieRepository : Repository<Homie>, IHomieRepository
    {
        public HomieRepository(HomiesContext context) : base(context)
        {
            
        }

        public void CheckIn(Homie homie, bool withGuest, DateTime time)
        {
            var checkInCollection = _dbContext.Entry(homie).Collection(x => x.CheckIns);
            if(!checkInCollection.IsLoaded)
            {
                checkInCollection.Load();
            }

            homie.CheckIns.Add(new CheckIn {
                Time = time,
                WithGuest = withGuest
            });
            
            _dbContext.SaveChanges();
        }
    }
}
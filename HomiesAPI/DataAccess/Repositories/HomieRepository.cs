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
            if (!checkInCollection.IsLoaded)
            {
                checkInCollection.Load();
            }

            homie.IsHome = true;
            homie.HasGuest = withGuest;
            homie.CheckIns.Add(new CheckIn
            {
                Time = time,
                WithGuest = withGuest
            });
            Edit(homie);
        }

        public void CheckOut(Homie homie, DateTime time)
        {
            var checkOutCollection = _dbContext.Entry(homie).Collection(x => x.CheckOuts);
            if (!checkOutCollection.IsLoaded)
            {
                checkOutCollection.Load();
            }

            homie.IsHome = false;
            homie.HasGuest = false;
            homie.CheckOuts.Add(new CheckOut{
                Time = time
            });
            Edit(homie);
        }
    }
}
using HomiesAPI.Models;

namespace HomiesAPI.DataAccess.Repositories
{
    public class CheckOutRepository : Repository<CheckOut>, ICheckOutRepository
    {
        public CheckOutRepository(HomiesContext context) : base(context)
        {
            
        }
    }
}
using HomiesAPI.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace HomiesAPI.DataAccess.Repositories
{
    public class CheckInRepository : Repository<CheckIn>
    {
        public CheckInRepository(HomiesContext context) : base(context)
        {
            
        }
    }
}
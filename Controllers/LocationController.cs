using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomiesAPI.Models;
using HomiesAPI.DataAccess.Repositories;
using System.Linq.Expressions;

namespace HomiesAPI.Controllers
{
    public class LocationController : ControllerBase
    {
        private ILocationRepository _locationRepo;

        private IHomieRepository _homieRepo;

        public LocationController(ILocationRepository locationRepo, IHomieRepository homieRepo)
        {
            _locationRepo = locationRepo;
            _homieRepo = homieRepo;
        }
    }
}
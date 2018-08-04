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
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationRepository _locationRepo;

        private IHomieRepository _homieRepo;

        public LocationController(ILocationRepository locationRepo, IHomieRepository homieRepo)
        {
            _locationRepo = locationRepo;
            _homieRepo = homieRepo;
        }

        [HttpGet]
        public ActionResult<List<Location>> GetAll()
        {
            var includes = new Expression<Func<Location, object>>[]{
                x => x.Homies
            };
            return _locationRepo.List(includes);
        }

        [HttpGet("{id}", Name = "GetLocationById")]
        public ActionResult<Location> GetById(int id)
        {
            var includes = new Expression<Func<Location, object>>[] {
                x => x.Homies
            };

            var location = _locationRepo.GetById(id, includes);
            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Location location)
        {
            _locationRepo.Add(location);

            return CreatedAtRoute("GetLocationById", new { id = location.Id }, location);
        }

        [HttpPut]
        public IActionResult FullUpdate([FromBody] Location updatedLocation)
        {
            _locationRepo.Edit(updatedLocation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var location = _locationRepo.GetById(id);
            if(location == null)
            {
                return NotFound();
            }

            _locationRepo.Delete(location);
            return NoContent();
        }

        [HttpPut("{id}/addhomie/{homieId}")]
        public IActionResult AddHomie(int id, int homieId)
        {
            var includes = new Expression<Func<Location, object>>[] {
                x => x.Homies
            };

            var location = _locationRepo.GetById(id, includes);
            if(location == null)
            {
                return NotFound();
            }

            var homie = _homieRepo.GetById(homieId);
            if(homie == null)
            {
                return NotFound();
            }

            location.Homies.Add(homie);
            _locationRepo.Edit(location);

            return NoContent();
        }
    }
}
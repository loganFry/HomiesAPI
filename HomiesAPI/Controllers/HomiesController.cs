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
    [Route("api/homies")]
    [ApiController]
    public class HomiesController : ControllerBase
    {
        private IHomieRepository _homieRepo;

        public HomiesController(IHomieRepository repo)
        {
            _homieRepo = repo;
        }

        [HttpGet]
        public ActionResult<List<Homie>> GetAll()
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.Location
            };
            var homies = _homieRepo.List(includes);

            return homies;
        }

        [HttpGet("{id}", Name = "GetHomieById")]
        public ActionResult<Homie> GetById(int id)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.Location
            };

            var homie = _homieRepo.GetById(id, includes);
            if (homie == null)
            {
                return NotFound();
            }

            return homie;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Homie homie)
        {
            _homieRepo.Add(homie);

            return CreatedAtRoute("GetHomieById", new { id = homie.Id }, homie);
        }

        [HttpPut("{id}")]
        public IActionResult FullUpdate([FromBody] Homie updatedHomie)
        {
            _homieRepo.Edit(updatedHomie);
            return NoContent();
        }

        [HttpPatch("{id}/checkin")]
        public IActionResult CheckIn(int id, [FromBody] bool withGuest)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckIns
            };
            var homie = _homieRepo.GetById(id, includes);
            if (homie == null)
            {
                return NotFound();
            }

            _homieRepo.CheckIn(homie, withGuest, DateTime.Now);

            return NoContent();
        }

        [HttpPatch("{id}/checkout")]
        public IActionResult CheckOut(int id)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckOuts
            };
            var homie = _homieRepo.GetById(id, includes);
            if (homie == null)
            {
                return NotFound();
            }

            _homieRepo.CheckOut(homie, DateTime.Now);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var homie = _homieRepo.GetById(id);
            if(homie == null)
            {
                return NotFound();
            }

            _homieRepo.Delete(homie);

            return NoContent();
        }
    }
}

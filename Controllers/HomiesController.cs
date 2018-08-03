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
        public ActionResult<IEnumerable<Homie>> GetAll()
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckIns,
                x => x.CheckOuts,
                x => x.Location
            };

            return _homieRepo.List(includes).ToList();
        }

        [HttpGet("{id}", Name = "GetHomieById")]
        public ActionResult<Homie> GetById(int id)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckIns,
                x => x.CheckOuts,
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
        public IActionResult FullUpdate(int id, [FromBody] Homie updatedHomie)
        {
            var homie = _homieRepo.GetById(id);
            if (homie == null)
            {
                return NotFound();
            }

            homie.FirstName = updatedHomie.FirstName;
            homie.LastName = updatedHomie.LastName;
            homie.NickName = updatedHomie.NickName;
            homie.Email = updatedHomie.Email;
            homie.IsHome = updatedHomie.IsHome;
            homie.HasGuest = updatedHomie.HasGuest;

            _homieRepo.Edit(homie);
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

            homie.IsHome = true;
            homie.HasGuest = withGuest;
            homie.CheckIns.Add(new CheckIn
            {
                WithGuest = withGuest,
                Time = DateTime.Now
            });
            _homieRepo.Edit(homie);

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

            homie.IsHome = false;
            homie.HasGuest = false;
            homie.CheckOuts.Add(new CheckOut{
                Time = DateTime.Now
            });
            _homieRepo.Edit(homie);
            
            return NoContent();
        }
    }
}

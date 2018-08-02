using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomiesAPI.Models;
using HomiesAPI.DataAccess.Repositories;

namespace HomiesAPI.Controllers
{    
    [Route("api/homies")]
    [ApiController]
    public class HomiesController : ControllerBase
    {
        private IHomieRepository _homiesRepo;

        public HomiesController(IHomieRepository repo)
        {
            _homiesRepo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Homie>> GetAll()
        {
            return _homiesRepo.List().ToList();
        }

        [HttpGet("{id}", Name="GetHomieById")]
        public ActionResult<Homie> GetById(int id)
        {
            var homie = _homiesRepo.GetById(id);
            if(homie == null){
                return NotFound();
            }

            return homie;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Homie homie)
        {
            _homiesRepo.Add(homie);

            return CreatedAtRoute("GetHomieById", new { id = homie.Id }, homie);
        }

        [HttpPut("{id}")]
        public IActionResult FullUpdate(int id, [FromBody] Homie updatedHomie)
        {
            var homie = _homiesRepo.GetById(id);
            if(homie == null)
            {
                return NotFound();
            }

            homie.FirstName = updatedHomie.FirstName;
            homie.LastName = updatedHomie.LastName;
            homie.NickName = updatedHomie.NickName;
            homie.Email = updatedHomie.Email;
            homie.IsHome = updatedHomie.IsHome;
            homie.HasGuest = updatedHomie.HasGuest;

            _homiesRepo.Edit(homie);
            return NoContent();
        }
    }
}

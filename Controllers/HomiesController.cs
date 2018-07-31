using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomiesAPI.DataAccess;
using HomiesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HomiesAPI.Controllers
{    
    [Route("api/homies")]
    [ApiController]
    public class HomiesController : ControllerBase
    {
        private HomiesContext _context;

        public HomiesController(HomiesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Homie>> GetAll()
        {
            return _context.Homies
                .Include(x => x.Location)
                .Include(x => x.CheckIns)
                .Include(x => x.CheckOuts).ToList();
        }

        [HttpGet("{id}", Name="GetHomieById")]
        public ActionResult<Homie> GetById(int id)
        {
            var homie = _context.Homies.FirstOrDefault(x => x.Id == id);
            if(homie == null){
                return NotFound();
            }
            _context.Entry(homie).Collection(x => x.CheckIns).Load();
            _context.Entry(homie).Collection(x => x.CheckOuts).Load();
            _context.Entry(homie).Reference(x => x.Location).Load();

            return homie;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Homie homie)
        {
            _context.Homies.Add(homie);
            _context.SaveChanges();

            return CreatedAtRoute("GetHomieById", new { id = homie.Id }, homie);
        }

        [HttpPut("{id}")]
        public IActionResult FullUpdate(int id, [FromBody] Homie updatedHomie)
        {
            var homie = _context.Homies.Find(new object[]{ id });
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

            _context.Homies.Update(homie);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

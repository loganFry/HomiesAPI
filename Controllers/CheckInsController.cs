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
    [Route("api")]
    public class CheckInsController : ControllerBase
    {
        private HomiesContext _context;

        public CheckInsController(HomiesContext context)
        {
            _context = context;
        }

        [HttpGet("checkins")]
        public ActionResult<IEnumerable<CheckIn>> GetAll()
        {
            return _context.CheckIns.ToList();
        }

        [HttpGet("checkins/{id}", Name="GetCheckInById")]
        public ActionResult<CheckIn> GetById(int id)
        {
            var checkIn = _context.CheckIns.FirstOrDefault(x => x.Id == id);
            if(checkIn == default(CheckIn))
            {
                return NotFound();
            }

            return checkIn;
        }

        [HttpPost("checkins")]
        public IActionResult Create([FromBody] CheckIn checkIn)
        {
            _context.CheckIns.Add(checkIn);
            _context.SaveChanges();

            return CreatedAtRoute("GetCheckInById", new { id = checkIn.Id }, checkIn);
        }

        [HttpGet("homies/{homieId}/checkins")]
        public ActionResult<IEnumerable<CheckIn>> GetAllForHomie(int homieId)
        {
            var homie = _context.Homies.FirstOrDefault(x => x.Id == homieId);
            if(homie == default(Homie))
            {
                return NotFound();
            }
            _context.Entry(homie).Collection(x => x.CheckIns).Load();

            return homie.CheckIns.ToList();
        }

        [HttpGet("homies/{homieId}/checkins/{id}", Name="GetHomieCheckInById")]
        public ActionResult<CheckIn> GetByIdForHomie(int homieId, int id)
        {
            var homie = _context.Homies.FirstOrDefault(x => x.Id == homieId);
            if(homie == default(Homie))
            {
                return NotFound();
            }
            _context.Entry(homie).Collection(x => x.CheckIns).Load();

            var checkIn = homie.CheckIns.FirstOrDefault(x => x.Id == id);
            if(checkIn == default(CheckIn))
            {
                return NotFound();
            }

            return checkIn;
        }
    }
}
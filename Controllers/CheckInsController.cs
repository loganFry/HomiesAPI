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
    }
}
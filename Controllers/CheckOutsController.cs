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
    public class CheckOutsController : ControllerBase
    {
        private HomiesContext _context;

        public CheckOutsController(HomiesContext context)
        {
            _context = context;
        }

        [HttpGet("{homieId}/checkouts")]
        public ActionResult<IEnumerable<CheckOut>> GetAll(int homieId)
        {
            var homie = _context.Homies.FirstOrDefault(x => x.Id == homieId);
            if(homie == default(Homie))
            {
                return NotFound();
            }
            _context.Entry(homie).Collection(x => x.CheckOuts).Load();

            return homie.CheckOuts.ToList();
        }
    }
}
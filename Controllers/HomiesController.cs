using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomiesAPI.DataAccess;
using HomiesAPI.Models;

namespace HomiesAPI.Controllers
{    
    [Route("[controller]")]
    [ApiController]
    public class HomiesController : ControllerBase
    {
        private HomiesContext _context;

        public HomiesController(HomiesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Homie>> Get()
        {
            return _context.Homies;
        }

        
    }
}

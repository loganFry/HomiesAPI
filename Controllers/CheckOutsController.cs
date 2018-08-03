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
    [Route("api")]
    public class CheckOutsController : ControllerBase
    {
        private ICheckOutRepository _checkOutRepo;

        private IHomieRepository _homieRepo;

        public CheckOutsController(ICheckOutRepository checkOutRepo, IHomieRepository homieRepo)
        {
            _checkOutRepo = checkOutRepo;
            _homieRepo = homieRepo;
        }

        [HttpGet("checkouts")]
        public ActionResult<IEnumerable<CheckOut>> GetAll(int homieId)
        {
            var checkOuts = _checkOutRepo.List().ToList();

            return checkOuts;
        }

        [HttpGet("checkouts/{id}", Name="GetCheckOutById")]
        public ActionResult<CheckOut> GetById(int id)
        {
            var checkOut = _checkOutRepo.GetById(id);
            if(checkOut == null)
            {
                return NotFound();
            }

            return checkOut;
        }

        [HttpGet("homies/{homieId}/checkouts")]
        public ActionResult<IEnumerable<CheckOut>> GetAllForHomie(int homieId)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckOuts
            };
            var homie = _homieRepo.GetById(homieId, includes);
            if(homie == null)
            {
                return NotFound();
            }

            return homie.CheckOuts.ToList();
        }

        [HttpGet("homies/{homieId}/checkouts/{id}", Name="GetHomieCheckOutById")]
        public ActionResult<CheckOut> GetByIdForHomie(int homieId, int id)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckOuts
            };
            var homie = _homieRepo.GetById(homieId, includes);
            if(homie == null)
            {
                return NotFound();
            }

            var checkIn = homie.CheckOuts.FirstOrDefault(x => x.Id == id);
            if(checkIn == null)
            {
                return NotFound();
            }

            return checkIn;
        }
    }
}
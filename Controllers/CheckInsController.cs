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
    public class CheckInsController : ControllerBase
    {
        private ICheckInRepository _checkInRepo;

        private IHomieRepository _homieRepo;

        public CheckInsController(ICheckInRepository checkInRepo, IHomieRepository homieRepo)
        {
            _checkInRepo = checkInRepo;
            _homieRepo = homieRepo;
        }

        [HttpGet("checkins")]
        public ActionResult<IEnumerable<CheckIn>> GetAll()
        {
            var checkins = _checkInRepo.List().ToList(); 

            return checkins;
        }

        [HttpGet("checkins/{id}", Name="GetCheckInById")]
        public ActionResult<CheckIn> GetById(int id)
        {
            var checkIn = _checkInRepo.GetById(id);
            if(checkIn == null)
            {
                return NotFound();
            }

            return checkIn;
        }

        [HttpDelete("checkins/{id}")]
        public IActionResult Delete(int id)
        {
            var checkIn = _checkInRepo.GetById(id);
            if(checkIn == null)
            {
                return NotFound();
            }

            _checkInRepo.Delete(checkIn);

            return NoContent();
        }

        [HttpGet("homies/{homieId}/checkins")]
        public ActionResult<IEnumerable<CheckIn>> GetAllForHomie(int homieId)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckIns
            };
            var homie = _homieRepo.GetById(homieId, includes);
            if(homie == null)
            {
                return NotFound();
            }

            return homie.CheckIns.ToList();
        }

        [HttpGet("homies/{homieId}/checkins/{id}", Name="GetHomieCheckInById")]
        public ActionResult<CheckIn> GetByIdForHomie(int homieId, int id)
        {
            var includes = new Expression<Func<Homie, object>>[] {
                x => x.CheckIns
            };
            var homie = _homieRepo.GetById(homieId, includes);
            if(homie == null)
            {
                return NotFound();
            }

            var checkIn = homie.CheckIns.FirstOrDefault(x => x.Id == id);
            if(checkIn == null)
            {
                return NotFound();
            }

            return checkIn;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}",  Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var result = _context.CelestialObjects.FirstOrDefault( x=> x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            var satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == id).ToList();
            result.Satellites = satellites;
            return Ok(result);
        }

        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _context.CelestialObjects.FirstOrDefault(x => x.Name == name);
            if (result == null)
            {
                return NotFound();
            }
            var satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == result.Id).ToList();
            result.Satellites = satellites;
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _context.CelestialObjects.ToList();

            foreach(var item in result)
            {
                var satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == item.Id).ToList();
                item.Satellites = satellites;
            }
            return Ok(result);
        }
    }
}

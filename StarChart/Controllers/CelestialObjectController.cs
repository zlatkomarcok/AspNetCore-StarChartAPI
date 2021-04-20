using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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
            var resultList = new List<CelestialObject>();
            resultList.Add(result);
            return Ok(resultList);
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

        [HttpPost]
        public IActionResult Create([FromBody]CelestialObject co)
        {
            _context.CelestialObjects.Add(co);
            _context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = co.Id }, co);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject co)
        {
            var found = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return NotFound();
            }
            found.Name = co.Name;
            found.OrbitalPeriod = co.OrbitalPeriod;
            found.OrbitedObjectId = co.OrbitedObjectId;
            _context.CelestialObjects.Update(found);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var found = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return NotFound();
            }
            found.Name = name;
            _context.CelestialObjects.Update(found);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var found = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            var items = _context.CelestialObjects.Where(x => x.Id == id || x.OrbitedObjectId == id);
            _context.CelestialObjects.RemoveRange(items);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandCarsController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public BrandCarsController(VehicleManagementContext context)
        {
            _context = context;
        }

        // GET: api/BrandCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandCar>>> GetBrandCars()
        {
          if (_context.BrandCars == null)
          {
              return NotFound();
          }
            return await _context.BrandCars.ToListAsync();
        }

        // GET: api/BrandCars/5
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<BrandCar>>> GetBrandCar(int id)
        {
            if (_context.BrandCars == null)
            {
                return NotFound();
            }

            List<BrandCar> cars  = await _context.BrandCars.Where(car => car.Brandid == id).ToListAsync();

            if (cars == null || cars.Count == 0)
            {
                return NotFound();
            }

            return cars;
        }
      







        // PUT: api/BrandCars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrandCar(int id, BrandCar brandCar)
        {
            if (id != brandCar.Carid)
            {
                return BadRequest();
            }

            _context.Entry(brandCar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandCarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BrandCars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandCar>> PostBrandCar(BrandCar brandCar)
        {
          if (_context.BrandCars == null)
          {
              return Problem("Entity set 'VehicleManagementContext.BrandCars'  is null.");
          }
            _context.BrandCars.Add(brandCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrandCar", new { id = brandCar.Carid }, brandCar);
        }

        // DELETE: api/BrandCars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandCar(int id)
        {
            if (_context.BrandCars == null)
            {
                return NotFound();
            }
            var brandCar = await _context.BrandCars.FindAsync(id);
            if (brandCar == null)
            {
                return NotFound();
            }

            _context.BrandCars.Remove(brandCar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandCarExists(int id)
        {
            return (_context.BrandCars?.Any(e => e.Carid == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarBrands1Controller : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public CarBrands1Controller(VehicleManagementContext context)
        {
            _context = context;
        }

        // GET: api/CarBrands1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarBrand>>> GetCarBrands()
        {
          if (_context.CarBrands == null)
          {
              return NotFound();
          }
            return await _context.CarBrands.ToListAsync();
        }

        // GET: api/CarBrands1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarBrand>> GetCarBrand(int id)
        {
          if (_context.CarBrands == null)
          {
              return NotFound();
          }
            var carBrand = await _context.CarBrands.FindAsync(id);

            if (carBrand == null)
            {
                return NotFound();
            }

            return carBrand;
        }

        // PUT: api/CarBrands1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarBrand(int id, CarBrand carBrand)
        {
            if (id != carBrand.Brandid)
            {
                return BadRequest();
            }

            _context.Entry(carBrand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarBrandExists(id))
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

        // POST: api/CarBrands1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarBrand>> PostCarBrand(CarBrand carBrand)
        {
          if (_context.CarBrands == null)
          {
              return Problem("Entity set 'VehicleManagementContext.CarBrands'  is null.");
          }
            _context.CarBrands.Add(carBrand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarBrand", new { id = carBrand.Brandid }, carBrand);
        }

        // DELETE: api/CarBrands1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarBrand(int id)
        {
            if (_context.CarBrands == null)
            {
                return NotFound();
            }
            var carBrand = await _context.CarBrands.FindAsync(id);
            if (carBrand == null)
            {
                return NotFound();
            }

            _context.CarBrands.Remove(carBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarBrandExists(int id)
        {
            return (_context.CarBrands?.Any(e => e.Brandid == id)).GetValueOrDefault();
        }
    }
}

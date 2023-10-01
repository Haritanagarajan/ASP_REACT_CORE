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
    public class CarFuelsController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public CarFuelsController(VehicleManagementContext context)
        {
            _context = context;
        }

        // GET: api/CarFuels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarFuel>>> GetCarFuels()
        {
          if (_context.CarFuels == null)
          {
              return NotFound();
          }
            return await _context.CarFuels.ToListAsync();
        }

        // GET: api/CarFuels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarFuel>> GetCarFuel(int id)
        {
          if (_context.CarFuels == null)
          {
              return NotFound();
          }
            var carFuel = await _context.CarFuels.FindAsync(id);

            if (carFuel == null)
            {
                return NotFound();
            }

            return carFuel;
        }

        // PUT: api/CarFuels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarFuel(int id, CarFuel carFuel)
        {
            if (id != carFuel.Fuelid)
            {
                return BadRequest();
            }

            _context.Entry(carFuel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarFuelExists(id))
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

        // POST: api/CarFuels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarFuel>> PostCarFuel(CarFuel carFuel)
        {
          if (_context.CarFuels == null)
          {
              return Problem("Entity set 'VehicleManagementContext.CarFuels'  is null.");
          }
            _context.CarFuels.Add(carFuel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarFuel", new { id = carFuel.Fuelid }, carFuel);
        }

        // DELETE: api/CarFuels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarFuel(int id)
        {
            if (_context.CarFuels == null)
            {
                return NotFound();
            }
            var carFuel = await _context.CarFuels.FindAsync(id);
            if (carFuel == null)
            {
                return NotFound();
            }

            _context.CarFuels.Remove(carFuel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarFuelExists(int id)
        {
            return (_context.CarFuels?.Any(e => e.Fuelid == id)).GetValueOrDefault();
        }
    }
}

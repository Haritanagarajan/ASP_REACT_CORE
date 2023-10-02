using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;
using VehicleManagement.Models.CarBrands;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarServicesController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public CarServicesController(VehicleManagementContext context)
        {
            _context = context;
        }

        // GET: api/CarServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarService>>> GetCarServices()
        {
            if (_context.CarServices == null)
            {
                return NotFound();
            }
            return await _context.CarServices.ToListAsync();
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CarService>>> GetCarService(int id)
        {
            if (_context.CarServices == null)
            {
                return NotFound();
            }

            var addAmount = _context.BrandCars
                .Select(brandCar => brandCar.AddAmount)
                .FirstOrDefault();

            List<CarService> service = await _context.CarServices
                .Where(carService => carService.Carid == id)
                .ToListAsync();

            if (service == null || service.Count == 0)
            {
                return NotFound();
            }

            // Add addAmount to the servicecost for each CarService
            foreach (var carService in service)
            {
                carService.Servicecost += addAmount;
            }

            return service;
        }


        // PUT: api/CarServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarService(int id, CarService carService)
        {
            if (id != carService.Serviceid)
            {
                return BadRequest();
            }

            _context.Entry(carService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarServiceExists(id))
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

        // POST: api/CarServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarService>> PostCarService(CarService carService)
        {
        //    if (_context.CarServices == null)
        //    {
        //        return Problem("Entity set 'VehicleManagementContext.CarServices'  is null.");
        //    }
            _context.CarServices.Add(carService);
            await _context.SaveChangesAsync();
            return StatusCode(201);
            //return CreatedAtAction("GetCarService", new { id = carService.Serviceid }, carService);
        }

        // DELETE: api/CarServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarService(int id)
        {
            if (_context.CarServices == null)
            {
                return NotFound();
            }
            var carService = await _context.CarServices.FindAsync(id);
            if (carService == null)
            {
                return NotFound();
            }

            _context.CarServices.Remove(carService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarServiceExists(int id)
        {
            return (_context.CarServices?.Any(e => e.Serviceid == id)).GetValueOrDefault();
        }
    }
}

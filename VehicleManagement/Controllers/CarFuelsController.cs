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
        private readonly IWebHostEnvironment _hostEnvironment;


        public CarFuelsController(VehicleManagementContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarFuel>>> GetCarFuels()
        {
            return await _context.CarFuels.Select(x => new CarFuel()
            {
                Fuelid = x.Fuelid,
                FuelName = x.FuelName,
                FuelImage = x.FuelImage,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.FuelImage)
            })
               .ToListAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carFuel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarFuel(int id, [FromForm] CarFuel carFuel)
        {
            if (id != carFuel.Fuelid)
            {
                return BadRequest();
            }
            if (carFuel.ImageFile != null)
            {
                carFuel.FuelImage = await SaveImage(carFuel.ImageFile);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carFuel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CarFuel>> PostCarFuel([FromForm] CarFuel carFuel)
        {
            carFuel.FuelImage = await SaveImage(carFuel.ImageFile);
            _context.CarFuels.Add(carFuel);
            await _context.SaveChangesAsync();
            return StatusCode(201);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            DeleteImage(carFuel.FuelImage);
            _context.CarFuels.Remove(carFuel);
            await _context.SaveChangesAsync();

            return Ok(carFuel);
        }

        private bool CarFuelExists(int id)
        {
            return (_context.CarFuels?.Any(e => e.Fuelid == id)).GetValueOrDefault();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}

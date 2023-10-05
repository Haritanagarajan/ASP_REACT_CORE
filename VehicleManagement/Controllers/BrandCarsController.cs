using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public BrandCarsController(VehicleManagementContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }


        /// <summary>
        /// Get Brands
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<ActionResult<IEnumerable<BrandCar>>> GetBrandCars()
        {
            return await _context.BrandCars.Select(x => new BrandCar()
            {
                Carid = x.Carid,
                Brandid = x.Brandid,
                CarName = x.CarName,
                AddAmount = x.AddAmount,
                CarImage = x.CarImage,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.CarImage)
            })
                .ToListAsync();
        }




        /// <summary>
        /// Get Brands brandid = brandid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<ActionResult<List<BrandCar>>> GetBrandCar(int id)
        {
            if (_context.BrandCars == null)
            {
                return NotFound();
            }
            List<BrandCar> cars = await _context.BrandCars
                .Where(car => car.Brandid == id)
                .Select(x => new BrandCar()
                {
                    Carid = x.Carid,
                    Brandid = x.Brandid,
                    CarName = x.CarName,
                    AddAmount = x.AddAmount,
                    CarImage = x.CarImage,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.CarImage)
                })
                .ToListAsync();

            if (cars == null || cars.Count == 0)
            {
                return NotFound();
            }
            return cars;
        }


        /// <summary>
        /// PutBrandCar 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandCar"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrandCar(int id, [FromForm] BrandCar brandCar)

        {
            if (id != brandCar.Carid)
            {
                return BadRequest();
            }
            if (brandCar.ImageFile != null)
            {
                brandCar.CarImage = await SaveImage(brandCar.ImageFile);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandCar"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<BrandCar>> PostBrandCar([FromForm] BrandCar brandCar)
        {
            brandCar.CarImage = await SaveImage(brandCar.ImageFile);
            _context.BrandCars.Add(brandCar);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

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
            DeleteImage(brandCar.CarImage);
            _context.BrandCars.Remove(brandCar);
            await _context.SaveChangesAsync();
            return Ok(brandCar);
        }


        private bool BrandCarExists(int id)
        {
            return (_context.BrandCars?.Any(e => e.Carid == id)).GetValueOrDefault();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        [NonAction]
        [Authorize(Roles = "Admin,Customer")]

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
        [Authorize(Roles = "Admin,Customer")]

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

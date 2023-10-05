﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarBrands1Controller(VehicleManagementContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }


        /// <summary>
        /// GetCarBrands
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]

        public async Task<ActionResult<IEnumerable<CarBrand>>> GetCarBrands()
        {
            return await _context.CarBrands.Select(x => new CarBrand()
            {
                Brandid = x.Brandid,
                BrandName = x.BrandName,
                BranndImage = x.BranndImage,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.BranndImage)
            })
                .ToListAsync();
        }

        /// <summary>
        /// GetCarBrand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carBrand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutCarBrand(int id, [FromForm] CarBrand carBrand)
        {
            if (id != carBrand.Brandid)
            {
                return BadRequest();
            }
            if (carBrand.ImageFile != null)
            {
                //DeleteImage(carBrand.BranndImage);
                carBrand.BranndImage = await SaveImage(carBrand.ImageFile);
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="carBrand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<CarBrand>> PostCarBrand([FromForm] CarBrand carBrand)
        {
            carBrand.BranndImage = await SaveImage(carBrand.ImageFile);
            _context.CarBrands.Add(carBrand);
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

        public async Task<IActionResult> DeleteCarBrand(int id)
        {
            var carBrand = await _context.CarBrands.FindAsync(id);
            if (carBrand == null)
            {
                return NotFound();
            }
            DeleteImage(carBrand.BranndImage);
            _context.CarBrands.Remove(carBrand);
            await _context.SaveChangesAsync();

            return Ok(carBrand);
        }

        private bool CarBrandExists(int id)
        {
            return (_context.CarBrands?.Any(e => e.Brandid == id)).GetValueOrDefault();
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

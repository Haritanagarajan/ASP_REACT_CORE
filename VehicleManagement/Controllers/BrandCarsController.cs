using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.IRepository;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandCarsController : ControllerBase
    {
        private readonly IBrandCar _brandCar;
        private readonly VehicleManagementContext _context;
        public BrandCarsController(IBrandCar brandCar, VehicleManagementContext context)
        {
            _brandCar = brandCar;
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<BrandCar>>> GetBrandCars()
        {
            try
            {
                var scheme = Request.Scheme;
                var host = Request.Host.ToUriComponent();
                var pathBase = Request.PathBase.ToUriComponent();
                var carbrands = await _brandCar.GetBrandCars(scheme, host, pathBase);
                return Ok(carbrands);
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Cars options.",
                });
            }
        }/// <summary>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Cars"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PutBrandCar(int id, [FromForm] BrandCar Cars)
        {
            try
            {
                await _brandCar.PutBrandCar(id, Cars);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Car Edit.",
                });
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Cars"></param>
       /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostBrandCar([FromForm] BrandCar Cars)
        {
            try
            {
                await _brandCar.PostBrandCar(Cars);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Car Post.",
                });
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrandCar(int id)
        {
            try
            {
                await _brandCar.DeleteBrandCar(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Car Delete.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> BrandCarExists(int id)
        {
            try
            {
                _brandCar.BrandCarExists(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Car Exists.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        [NonAction]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> DeleteImage(string imageName)
        {
            try
            {
                _brandCar.DeleteImage(imageName);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Car Image Delete.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        [NonAction]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> SaveImage(IFormFile imageFile)
        {
            try
            {
                await _brandCar.SaveImage(imageFile);
                return Ok();

            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Car Image Save.",
                });
            }
        }
    }
}

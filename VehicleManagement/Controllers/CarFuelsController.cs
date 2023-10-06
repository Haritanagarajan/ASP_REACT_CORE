using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.IRepository;
using VehicleManagement.Models;
using VehicleManagement.Repository;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarFuelsController : ControllerBase
    {
        private readonly ICarFuel _carfuel;
        private readonly VehicleManagementContext _context;
        public CarFuelsController(ICarFuel carfuel, VehicleManagementContext context)
        {
            _carfuel = carfuel;
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<CarFuel>>> GetCarFuels()
        {
            try
            {
                var scheme = Request.Scheme;
                var host = Request.Host.ToUriComponent();
                var pathBase = Request.PathBase.ToUriComponent();
                var carFuels = await _carfuel.GetCarFuels(scheme, host, pathBase);
                return Ok(carFuels);
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel options.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carFuel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PutCarFuel(int id, [FromForm]CarFuel carFuel)
        {
            try
            {
                await _carfuel.PutCarFuel(id, carFuel);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel Edit.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carFuel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostCarFuel([FromForm] CarFuel carFuel)
        {
            try
            {
                await _carfuel.PostCarFuel(carFuel);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel Post.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarFuel(int id)
        {
            try
            {
                await _carfuel.DeleteCarFuel(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel Delete.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CarFuelExists(int id)
        {
            try
            {
                 _carfuel.CarFuelExists(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel Exists.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        [NonAction]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> DeleteImage(string imageName)
        {
            try
            {
                  _carfuel.DeleteImage(imageName);
                    return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel Image Delete.",
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
                await _carfuel.SaveImage(imageFile);
                return Ok();

            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Fuel Image Save.",
                });
            }
        }
    }
}

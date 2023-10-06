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
    public class CarBrands1Controller : ControllerBase
    {
        private readonly ICarBrand _carbrand;
        private readonly VehicleManagementContext _context;
        public CarBrands1Controller(ICarBrand carbrand, VehicleManagementContext context)
        {
            _carbrand = carbrand;
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<CarBrand>>> GetCarBrands()
        {
            try
            {
                var scheme = Request.Scheme;
                var host = Request.Host.ToUriComponent();
                var pathBase = Request.PathBase.ToUriComponent();
                var carbrands = await _carbrand.GetCarBrands(scheme, host, pathBase);
                return Ok(carbrands);
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Brands options.",
                });
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carBrand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PutCarBrand(int id, [FromForm] CarBrand carBrand)
        {
            try
            {
                await _carbrand.PutCarBrand(id, carBrand);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid brand Edit.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carBrand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostCarBrand([FromForm] CarBrand carBrand)
        {
            try
            {
                await _carbrand.PostCarBrand(carBrand);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid brand Post.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarBrand(int id)
        {
            try
            {
                await _carbrand.DeleteCarBrand(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid brand Delete.",
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CarBrandExists(int id)
        {
            try
            {
                _carbrand.CarBrandExists(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid brand Exists.",
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
                _carbrand.DeleteImage(imageName);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid brand Image Delete.",
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
                await _carbrand.SaveImage(imageFile);
                return Ok();

            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid brand Image Save.",
                });
            }
        }
    }
}

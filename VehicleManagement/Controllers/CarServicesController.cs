using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Octokit;
using VehicleManagement.Interface;
using VehicleManagement.IRepository;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarServicesController : ControllerBase
    {
        private readonly ICarService _servicerepo;
        private readonly VehicleManagementContext _context;
        public CarServicesController(ICarService servicerepo, VehicleManagementContext context)
        {
            _servicerepo = servicerepo;
            _context = context;
        }
        /// <summary>
        /// gets services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<CarService>>> GetCarServices()
        {
            try
            {
                var carServices = await _servicerepo.GetCarServices();
                return Ok(carServices); 
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Service.",
                });
            }
        }

        /// <summary>
        /// gets services based on matching carid(addamount) += id(servicecost)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<CarService>>> GetCarService(int id)
        {
            try
            {
                var carserviceid = await _servicerepo.GetCarService(id);
                return Ok(carserviceid);
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Service.",
                });
            }
        }
        /// <summary>
        /// edits services
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carService"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCarService(int id, CarService carService)
        {
            try
            {
                await _servicerepo.PutCarService(id, carService);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Service Edit.",
                });

            }
        }
        /// <summary>
        /// creates services
        /// </summary>
        /// <param name="carService"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCarService(CarService carService)
        {
            try
            {
                await _servicerepo.PostCarService(carService);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Service Post.",
                });
            }
        }
        /// <summary>
        /// deleted services based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarService(int id)
        {
            try
            {
                await _servicerepo.DeleteCarService(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Service Delete.",
                });
            }  
        }
        /// <summary>
        /// checks whether services exists or not and returns bool value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CarServiceExists(int id)
        {
            try
            {
                _servicerepo.CarServiceExists(id);
                return Ok();
            }
            catch
            {
                return BadRequest(new Models.Response
                {
                    Status = "Invalid",
                    Message = "Invalid Service Exists.",
                });
            }
        }
    }
}

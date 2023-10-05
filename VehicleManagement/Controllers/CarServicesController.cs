using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public CarServicesController(ICarService servicerepo)
        {
            _servicerepo = servicerepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<IEnumerable<CarService>> GetCarServices()
        {
            return _servicerepo.GetCarServices();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<IEnumerable<CarService>> GetCarService(int id)
        {
            return  _servicerepo.GetCarService(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carService"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult PutCarService(int id, CarService carService)
        {
            return _servicerepo.PutCarService(id, carService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carService"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<CarService> PostCarService(CarService carService)
        {
            return _servicerepo.PostCarService(carService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]

        public IActionResult DeleteCarService(int id)
        {
            return _servicerepo.DeleteCarService(id);
        }

        [HttpGet]
        public bool CarServiceExists(int id)
        {
            return _servicerepo.CarServiceExists(id);
        }
    }
}

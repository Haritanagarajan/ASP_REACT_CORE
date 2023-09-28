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
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandsController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public CarBrandsController(VehicleManagementContext context)
        {
            _context = context;
        }

        // private ICarBrand _ICarBrand;

        //public CarBrandsController()
        //{
        //    this._ICarBrand = new CarBrandRepo(new VehicleManagementContext());
        //}

        // GET: api/CarBrands
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<CarBrand>>> ListMethod()
        //{

        //    var list = _ICarBrand.GetCarBrand();

        //    return Ok(list);
        //}


        public async Task<ActionResult<IEnumerable<CarBrand>>> GetVusers()
        {
            if (_context.CarBrands == null)
            {
                return NotFound();
            }
            return await _context.CarBrands.ToListAsync();
        }

        // GET: api/CarBrands/5
        //[HttpGet("{id}")]
        //public ActionResult Get(int id)
        //{

        //    var carBrand = _ICarBrand.GetMethod(id);

        //    return Ok(carBrand);
        //}




        // PUT: api/CarBrands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public IActionResult Edit(CarBrand carBrand)
        //{

        //    _ICarBrand.UpdateBrand(carBrand);
        //    _ICarBrand.SaveChanges();   
        //    return Ok();
        //}

        // POST: api/CarBrands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public  IActionResult Create(CarBrand carBrand)
        //{

        //    _ICarBrand.InsertBrand(carBrand);
        //     _ICarBrand.SaveChanges();

        //    return CreatedAtAction("GetMthod", new { id = carBrand.Brandid }, carBrand);
        //}


        // DELETE: api/CarBrands/5
        //[HttpDelete("{id}")]
        //public  IActionResult Delete(int id)
        //{
        //    _ICarBrand.DeletBrand(id);
        //    _ICarBrand.SaveChanges();
        //    return Ok();
        //}


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Octokit.Internal;
using Swashbuckle.AspNetCore.SwaggerGen;
using VehicleManagement.Models;
using VehicleManagement.Models.CarBrands;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarBrandsController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        //public static IWebHostEnvironment _environment;


        public CarBrandsController(VehicleManagementContext context)
        {
            _context = context;
        }

    //    [HttpPost, DisableRequestSizeLimit]
    //    private IActionResult Upload()
    //    {
    //        Response res = new Response();
    //        try
    //        {
    //            var file = Request.Form.Files[0];
    //            var folderName = Path.Combine("wwwroot", "Assets");
    //            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

    //            if (file.Length > 0)
    //            {
    //                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
    //                var fullPath = Path.Combine(pathToSave, fileName);
    //                var dbPath = Path.Combine(folderName, fileName);

    //                using (var stream = new FileStream(fullPath, FileMode.Create))
    //                {
    //                    file.CopyTo(stream);
    //                }
    //                return Ok(new { dbPath });
    //            }
    //            else
    //            {
    //                return BadRequest();
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            res.Message = ex.Message;
    //        }
    //        return Ok(res);
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<CarBrand>>> GetCarBrand()
    //    {
    //        if (_context.CarBrands == null)
    //        {
    //            return NotFound();
    //        }
    //        return await _context.CarBrands.ToListAsync();
    //    }


    }

}

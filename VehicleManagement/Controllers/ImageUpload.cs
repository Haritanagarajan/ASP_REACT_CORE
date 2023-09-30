using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

#nullable disable

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageUpload : ControllerBase
    {
        private readonly VehicleManagementContext _context;


        public static IWebHostEnvironment _environment;

        public ImageUpload(IWebHostEnvironment environment, VehicleManagementContext context)
        {
            _environment = environment;
            _context = context;
        }

        [HttpPost]
        public IActionResult Post( CarBrandUpload objFile)
        {
            CarBrandUpload obj = new CarBrandUpload();

            try
            {
                obj.Brandid = objFile.Brandid;
                obj.BranndImage = "\\Assets\\" + objFile.files.FileName;
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Assets"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Assets\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Assets\\" + objFile.files.FileName))
                    {
                        string imagePath = _environment.WebRootPath + "\\Assets\\" + objFile.files.FileName;
                        objFile.files.CopyTo(filestream);
                        filestream.Flush();
                        objFile.BranndImage = imagePath;

                        CarBrand car = new CarBrand
                        {
                            BrandName = objFile.BrandName,
                            BranndImage = objFile.BranndImage
                        };

                        try
                        {
                            _context.CarBrands.Add(car);
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            BadRequest(ex.Message);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }
    }
}


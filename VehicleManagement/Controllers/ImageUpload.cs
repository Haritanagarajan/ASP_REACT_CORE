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
        private readonly VehicleManagementContext _context ;
      

        public static IWebHostEnvironment _environment;
        public ImageUpload(IWebHostEnvironment environment,VehicleManagementContext context)
        {
            _environment = environment;
            _context = context;
        }

        [HttpPost]
        public Task<Common> Post([FromBody]CarBrandUpload objFile)
        {
            Common obj = new Common();
            obj._fileAPI = new CarBrandUpload();

            try
            {
                obj._fileAPI.Brandid1 = objFile.Brandid1;
                obj._fileAPI.BranndImage1 = "\\Assets\\" + objFile.files.FileName;
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
                        objFile.BranndImage1=imagePath;

                        CarBrand car = new CarBrand
                        {
                           Brandid = objFile.Brandid1,
                           BrandName = objFile.BrandName1,
                           BranndImage = objFile.BranndImage1
                        };
                       
                        try
                        {
                            _context.CarBrands.Add(car);
                            _context.SaveChanges();
                        }
                        catch(Exception ex)
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
            return Task.FromResult(obj);
        }
    }
}


using Azure.Core;
using Microsoft.Extensions.Hosting;
using VehicleManagement.IRepository;
using VehicleManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace VehicleManagement.Repository
{
    public class CarFuelRepo : ICarFuel
    {
        private readonly VehicleManagementContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CarFuelRepo(VehicleManagementContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="host"></param>
        /// <param name="pathBase"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CarFuel>> GetCarFuels(string scheme, string host, string pathBase)
        {
            return await _context.CarFuels.Select(x => new CarFuel()
            {
                Fuelid = x.Fuelid,
                FuelName = x.FuelName,
                FuelImage = x.FuelImage,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", scheme, host, pathBase, x.FuelImage)
            })
            .ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carFuel"></param>
        /// <returns></returns>
        public async Task PutCarFuel(int id, [FromForm] CarFuel carFuel)
        {
            if (carFuel.ImageFile != null)
            {
                carFuel.FuelImage = await SaveImage(carFuel.ImageFile);
            }
            _context.Entry(carFuel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carFuel"></param>
        /// <returns></returns>
        public async Task PostCarFuel([FromForm] CarFuel carFuel)
        {
            carFuel.FuelImage = await SaveImage(carFuel.ImageFile);
            _context.CarFuels.Add(carFuel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCarFuel(int id)
        {
            var carfuel = await _context.CarFuels.FindAsync(id);
            DeleteImage(carfuel.FuelImage);
            _context.CarFuels.Remove(carfuel);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CarFuelExists(int id)
        {
            return (_context.CarFuels?.Any(e => e.Fuelid == id)).GetValueOrDefault();
        }
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VehicleManagement.IRepository;
using VehicleManagement.Models;

namespace VehicleManagement.Repository
{
    public class BrandCarRepo : IBrandCar
    {
        private readonly VehicleManagementContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BrandCarRepo(VehicleManagementContext context, IWebHostEnvironment hostEnvironment)
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
        public async Task<IEnumerable<BrandCar>> GetBrandCars(string scheme, string host, string pathBase)
        {
            return await _context.BrandCars.Select(x => new BrandCar()
            {
                Carid = x.Carid,
                Brandid = x.Brandid,
                CarName = x.CarName,
                AddAmount = x.AddAmount,
                CarImage = x.CarImage,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", scheme, host, pathBase, x.CarImage)
            })
            .ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandCar"></param>
        /// <returns></returns>
        public async Task PutBrandCar(int id, [FromForm] BrandCar brandCar)
        {
            if (brandCar.ImageFile != null)
            {
                brandCar.CarImage = await SaveImage(brandCar.ImageFile);
            }
            _context.Entry(brandCar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandCar"></param>
        /// <returns></returns>
        public async Task PostBrandCar([FromForm] BrandCar brandCar)
        {
            brandCar.CarImage = await SaveImage(brandCar.ImageFile);
            _context.BrandCars.Add(brandCar);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteBrandCar(int id)
        {
            var brandCar = await _context.BrandCars.FindAsync(id);
            DeleteImage(brandCar.CarImage);
            _context.BrandCars.Remove(brandCar);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BrandCarExists(int id)
        {
            return (_context.BrandCars?.Any(e => e.Carid == id)).GetValueOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageName"></param>
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

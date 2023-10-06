using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.IRepository;
using VehicleManagement.Models;

namespace VehicleManagement.Repository
{
    public class CarBrandRepo : ICarBrand
    {
        private readonly VehicleManagementContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CarBrandRepo(VehicleManagementContext context, IWebHostEnvironment hostEnvironment)
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
        public async Task<IEnumerable<CarBrand>> GetCarBrands(string scheme, string host, string pathBase)
        {
            return await _context.CarBrands.Select(x => new CarBrand()
            {
                Brandid = x.Brandid,
                BrandName = x.BrandName,
                BranndImage = x.BranndImage,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", scheme, host, pathBase, x.BranndImage)
            })
            .ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carBrand"></param>
        /// <returns></returns>
        public async Task PutCarBrand(int id, [FromForm] CarBrand carBrand)
        {
            if (carBrand.ImageFile != null)
            {
                carBrand.BranndImage = await SaveImage(carBrand.ImageFile);
            }
            _context.Entry(carBrand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carBrand"></param>
        /// <returns></returns>
        public async Task PostCarBrand([FromForm] CarBrand carBrand)
        {
            carBrand.BranndImage = await SaveImage(carBrand.ImageFile);
            _context.CarBrands.Add(carBrand);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCarBrand(int id)
        {
            var carBrand = await _context.CarBrands.FindAsync(id);
            DeleteImage(carBrand.BranndImage);
            _context.CarBrands.Remove(carBrand);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CarBrandExists(int id)
        {
            return (_context.CarBrands?.Any(e => e.Brandid == id)).GetValueOrDefault();
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

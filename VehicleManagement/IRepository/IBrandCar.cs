using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.IRepository
{
    public interface IBrandCar
    {
        public Task<IEnumerable<BrandCar>> GetBrandCars(string scheme, string host, string pathBase);
        public Task PutBrandCar(int id, [FromForm] BrandCar brandCar);
        public Task PostBrandCar([FromForm] BrandCar brandCar);
        public Task DeleteBrandCar(int id);
        public bool BrandCarExists(int id);
        public void DeleteImage(string imageName);
        public Task<string> SaveImage(IFormFile imageFile);
    }
}

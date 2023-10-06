using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.IRepository
{
    public interface ICarBrand
    {
        public Task<IEnumerable<CarBrand>> GetCarBrands(string scheme, string host, string pathBase);
        public Task PutCarBrand(int id, [FromForm] CarBrand carBrand);
        public Task PostCarBrand([FromForm] CarBrand carBrand);
        public Task DeleteCarBrand(int id);
        public bool CarBrandExists(int id);
        public void DeleteImage(string imageName);
        public Task<string> SaveImage(IFormFile imageFile);
    }
}

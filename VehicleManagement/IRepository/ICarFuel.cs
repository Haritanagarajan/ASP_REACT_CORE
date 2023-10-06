using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.IRepository
{
    public interface ICarFuel
    {
        public Task<IEnumerable<CarFuel>> GetCarFuels(string scheme, string host, string pathBase);
        public Task PutCarFuel(int id, [FromForm] CarFuel carFuel);
        public Task PostCarFuel([FromForm] CarFuel carFuel);
        public Task DeleteCarFuel(int id);
        public bool CarFuelExists(int id);
        public void DeleteImage(string imageName);
        public  Task<string> SaveImage(IFormFile imageFile);
    }
}

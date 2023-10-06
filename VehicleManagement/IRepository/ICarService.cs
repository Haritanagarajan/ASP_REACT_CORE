using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.IRepository
{
    public interface ICarService
    {
        public Task<IEnumerable<CarService>> GetCarServices();
        public Task<IEnumerable<CarService>> GetCarService(int id);
        public Task PutCarService(int id, CarService carService);
        public Task PostCarService(CarService carService);
        public Task DeleteCarService(int id);
        public bool CarServiceExists(int id);

    }
}

using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.IRepository
{
    public interface ICarService
    {
        ActionResult<IEnumerable<CarService>> GetCarServices();
        ActionResult<IEnumerable<CarService>> GetCarService(int id);
        IActionResult PutCarService(int id, CarService carService);
        ActionResult<CarService> PostCarService(CarService carService);
        IActionResult DeleteCarService(int id);
        public bool CarServiceExists(int id);
    }
}

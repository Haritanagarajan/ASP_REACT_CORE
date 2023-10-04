using Microsoft.AspNetCore.Mvc;
using VehicleManagement.IRepository;
using VehicleManagement.Models;
using VehicleManagement.Interface;
using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.Repository
{
    public class CarServiceRepo : ICarService
    {
        private readonly VehicleManagementContext _context;
        private readonly IConfiguration _configuration;

        public CarServiceRepo(VehicleManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ActionResult<IEnumerable<CarService>> GetCarServices()
        {
            if (_context.CarServices == null)
            {
                return null;
            }
            return _context.CarServices.ToList();
        }

        public ActionResult<IEnumerable<CarService>> GetCarService(int id)
        {
            var addAmount = _context.BrandCars
                .Select(brandCar => brandCar.AddAmount)
                .FirstOrDefault();
            List<CarService> service = _context.CarServices
                .Where(carService => carService.Carid == id)
                .ToList();
            if (service == null || service.Count == 0)
            {
                return null;
            }
            foreach (var carService in service)
            {
                carService.Servicecost += addAmount;
            }
            return service;
        }


        public IActionResult PutCarService(int id, CarService carService)
        {
            _context.Entry(carService).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarServiceExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        public ActionResult<CarService> PostCarService(CarService carService)
        {
            _context.CarServices.Add(carService);
            _context.SaveChangesAsync();
            return null;
        }

        public IActionResult DeleteCarService(int id)
        {
            var carService = _context.CarServices.Find(id);
            _context.CarServices.Remove(carService);
            _context.SaveChangesAsync();
            return null ;
        }

        public bool CarServiceExists(int id)
        {
            return (_context.CarServices?.Any(e => e.Serviceid == id)).GetValueOrDefault();
        }


    }
}

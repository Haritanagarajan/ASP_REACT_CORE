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
        public CarServiceRepo(VehicleManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// get all the services
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CarService>> GetCarServices()
        {
            return await _context.CarServices.ToListAsync();
        }
        /// <summary>
        /// get all services based matching carid(addamount) += id(servicecost)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CarService>> GetCarService(int id) //IEnumerable represnts list of items we can loop through collections of data
        {
            var addAmount = _context.BrandCars
                .Select(brandCar => brandCar.AddAmount)
                .FirstOrDefault();
            List<CarService> service = await _context.CarServices
                .Where(carService => carService.Carid == id)
                .ToListAsync();
            foreach (var carService in service)
            {
                carService.Servicecost += addAmount;
            }
            return service;
        }
        /// <summary>
        /// edits the services
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carService"></param>
        /// <returns></returns>
        public async Task PutCarService(int id, CarService carService)
        {
            _context.Entry(carService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// creates car services
        /// </summary>
        /// <param name="carService"></param>
        /// <returns></returns>
        public async Task PostCarService(CarService carService)
        {
            _context.CarServices.Add(carService);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// deletes service based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCarService(int id)
        {
            var carService = await _context.CarServices.FindAsync(id);
            _context.CarServices.Remove(carService);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// checks whether service id record exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CarServiceExists(int id)
        {
            return (_context.CarServices?.Any(e => e.Serviceid == id)).GetValueOrDefault();
        }
    }
}

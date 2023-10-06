using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Interface;
using VehicleManagement.Models;

namespace VehicleManagement.Repository
{
    public class CarDetailsRepo : ICarDetails
    {
        private readonly VehicleManagementContext _context;
        private readonly IConfiguration _configuration;
        public CarDetailsRepo(VehicleManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carDetail"></param>
        /// <returns></returns>
        public async Task<ActionResult<CarDetail>> PostCarDetail(CarDetail carDetail)
        {
            _context.CarDetails.Add(carDetail);
            await _context.SaveChangesAsync();
            return carDetail;
        }
    }
}

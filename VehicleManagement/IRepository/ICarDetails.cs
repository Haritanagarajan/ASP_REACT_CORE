using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.Interface
{
    public interface ICarDetails
    {
        Task<ActionResult<CarDetail>> PostCarDetail(CarDetail carDetail);
    }
}

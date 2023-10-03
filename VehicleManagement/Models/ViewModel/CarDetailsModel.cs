using System.Drawing;

namespace VehicleManagement.Models.ViewModel
{
    public class CarDetailsModel
    {

        public IEnumerable<BrandCar> BrandCar { get; set; }
        public IEnumerable<CarBrand> CarBrand { get; set; }
        public IEnumerable<CarService> CarService { get; set; }
        public IEnumerable<Vuser> Vuser { get; set; }
    }
}

namespace VehicleManagement.Models
{
    public class CarBrandUpload
    {
        public int Brandid { get; set; }

        public string BrandName { get; set; }

        public string BranndImage { get; set; }

        public IFormFile files { get; set; }


    }
}




namespace VehicleManagement.Models.CarBrands
{
    internal interface ICarBrand
    {
        Task<IEnumerable<CarBrand>> GetCarBrand();
        CarBrand GetMethod(int brandid);
        void InsertBrand(CarBrand brand);
        void UpdateBrand(CarBrand brand);
        void DeletBrand(int brandid);
        void SaveChanges();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.Models.CarBrands
{
    public class CarBrandRepo : ICarBrand
    {
        private readonly VehicleManagementContext _context;


        public CarBrandRepo(VehicleManagementContext _context)
        {
            this._context = _context;
        }
        public void DeletBrand(int brandid)
        {
            CarBrand brand = _context.CarBrands.Find(brandid);
            _context.CarBrands.Remove(brand);
        }
        public CarBrand GetMethod(int brandid)
        {
            return _context.CarBrands.Find(brandid);
        }
        public async Task<IEnumerable<CarBrand>> GetCarBrand()
        {
            return await _context.CarBrands.ToListAsync();
        }
       
        public void InsertBrand(CarBrand brand)
        {
            _context.CarBrands.Add(brand);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void UpdateBrand(CarBrand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
        }
    }
}


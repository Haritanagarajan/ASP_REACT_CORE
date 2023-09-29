using System;
using System.Collections.Generic;

namespace VehicleManagement.Models;

public partial class CarBrand
{
    public int Brandid { get; set; }

    public string? BrandName { get; set; }

    public string? BranndImage { get; set; }

    public virtual ICollection<BrandCar> BrandCars { get; set; } = new List<BrandCar>();

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

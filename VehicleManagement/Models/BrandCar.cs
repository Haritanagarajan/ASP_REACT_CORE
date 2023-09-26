using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models;

public partial class BrandCar
{
    [Key]
    public int Carid { get; set; }

    public int? Brandid { get; set; }

    public string? CarName { get; set; }

    public decimal? AddAmount { get; set; }

    public byte[]? CarImage { get; set; }

    public virtual CarBrand? Brand { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();

    public virtual ICollection<CarService> CarServices { get; set; } = new List<CarService>();
}

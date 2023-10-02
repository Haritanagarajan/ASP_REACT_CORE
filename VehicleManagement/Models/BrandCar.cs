using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models;

public partial class BrandCar
{
    public int Carid { get; set; }

    public int? Brandid { get; set; }

    public string? CarName { get; set; }

    public decimal? AddAmount { get; set; }

    public string? CarImage { get; set; }


    [NotMapped]
    public IFormFile? ImageFile { get; set; }


    [NotMapped]
    public string? ImageSrc { get; set; }

    public virtual CarBrand? Brand { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();

    public virtual ICollection<CarService> CarServices { get; set; } = new List<CarService>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models;

public partial class CarFuel
{
    public int Fuelid { get; set; }

    public string? FuelName { get; set; }

    public string? FuelImage { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }


    [NotMapped]
    public string? ImageSrc { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

using System;
using System.Collections.Generic;

namespace VehicleManagement.Models;

public partial class CarFuel
{
    public int Fuelid { get; set; }

    public string? FuelName { get; set; }

    public string? FuelImage { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

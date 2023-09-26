using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models;

public partial class CarFuel
{
    [Key]

    public int Fuelid { get; set; }

    public string? FuelName { get; set; }

    public byte[]? FuelImage { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

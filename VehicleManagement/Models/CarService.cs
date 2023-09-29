using System;
using System.Collections.Generic;

namespace VehicleManagement.Models;

public partial class CarService
{
    public int Serviceid { get; set; }

    public int? Carid { get; set; }

    public string? ServiceName { get; set; }

    public string? Warranty { get; set; }

    public string? Subservice1 { get; set; }

    public string? Subservice2 { get; set; }

    public string? Subservice3 { get; set; }

    public string? Subservice4 { get; set; }

    public short? TimeTaken { get; set; }

    public decimal? Servicecost { get; set; }

    public virtual BrandCar? Car { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

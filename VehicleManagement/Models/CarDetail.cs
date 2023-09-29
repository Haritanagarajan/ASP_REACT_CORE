﻿using System;
using System.Collections.Generic;

namespace VehicleManagement.Models;

public partial class CarDetail
{
    public int DetailsId { get; set; }

    public int? Vuserid { get; set; }

    public int? Brandid { get; set; }

    public int? Carid { get; set; }

    public int? Fuelid { get; set; }

    public int? Serviceid { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public virtual CarBrand? Brand { get; set; }

    public virtual BrandCar? Car { get; set; }

    public virtual CarFuel? Fuel { get; set; }

    public virtual CarService? Service { get; set; }

    public virtual Vuser? Vuser { get; set; }
}

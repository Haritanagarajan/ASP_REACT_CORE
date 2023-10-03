using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models;

public partial class CarDetail
{
    [Key]
    public int DetailsId { get; set; }

    public int? Vuserid { get; set; }

    public string? VuserName { get; set; }

    public int? Brandid { get; set; }

    public string? BrandImage { get; set; }

    public int? Carid { get; set; }

    public string? CarImage { get; set; }

    public int? Fuelid { get; set; }

    public string? FuelImage { get; set; }

    public int? Serviceid { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public virtual CarBrand? Brand { get; set; }

    public virtual BrandCar? Car { get; set; }

    public virtual CarFuel? Fuel { get; set; }

    public virtual CarService? Service { get; set; }

    public virtual Vuser? Vuser { get; set; }
}

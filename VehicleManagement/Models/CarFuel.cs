using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models;

public partial class CarFuel
{
    [Key]
    public int Fuelid { get; set; }
    [Required(ErrorMessage = "Fuel Name is required")]
    public string? FuelName { get; set; }
    [Required(ErrorMessage = "Fuel Name is required")]
    public string? FuelImage { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    public string? ImageSrc { get; set; }
    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models;

public partial class BrandCar
{
    [Key]
    public int Carid { get; set; }
    [Required(ErrorMessage = "Brandid is required")]
    public int? Brandid { get; set; }
    [Required(ErrorMessage = "CarName is required")]
    public string? CarName { get; set; }
    [Required(ErrorMessage = "Amount is required")]
    public decimal? AddAmount { get; set; }
    [Required(ErrorMessage = "CarImage is required")]
    public string? CarImage { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    public string? ImageSrc { get; set; }
    public virtual CarBrand? Brand { get; set; }
    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
    public virtual ICollection<CarService> CarServices { get; set; } = new List<CarService>();
}

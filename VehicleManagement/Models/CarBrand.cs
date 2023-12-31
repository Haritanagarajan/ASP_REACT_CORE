﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models;

public partial class CarBrand
{
    [Key]
    public int Brandid { get; set; }
    //[Required(ErrorMessage = "BrandName is required")]
    public string? BrandName { get; set; }
    //[Required(ErrorMessage = "Image is required")]
    public string? BranndImage { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    public string? ImageSrc { get; set; }
    public virtual ICollection<BrandCar> BrandCars { get; set; } = new List<BrandCar>();
    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

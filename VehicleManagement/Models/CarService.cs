using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models;

public partial class CarService
{
    [Key]
    public int Serviceid { get; set; }
    [Required(ErrorMessage = "Car Id is required")]
    public int? Carid { get; set; }
    [Required(ErrorMessage = "Service Name  is required")]
    public string? ServiceName { get; set; }
    [Required(ErrorMessage = "Warranty is required")]
    public string? Warranty { get; set; }
    [Required(ErrorMessage = "Sub service1 is required")]
    public string? Subservice1 { get; set; }
    [Required(ErrorMessage = "Sub service2 is required")]
    public string? Subservice2 { get; set; }
    [Required(ErrorMessage = "Sub service3 is required")]
    public string? Subservice3 { get; set; }
    [Required(ErrorMessage = "Sub service4 is required")]
    public string? Subservice4 { get; set; }
    [Required(ErrorMessage = "TimeTaken is required")]
    public short? TimeTaken { get; set; }
    [Required(ErrorMessage = "Servicecost is required")]
    public decimal? Servicecost { get; set; }
    public virtual BrandCar? Car { get; set; }
    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();
}

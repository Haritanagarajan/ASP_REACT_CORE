using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class ValidateCheck
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? vusername {  get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? vpassword { get; set; }
    }
}

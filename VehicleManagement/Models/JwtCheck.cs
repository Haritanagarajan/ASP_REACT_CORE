using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class JwtCheck
    {
        //[Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        //[Required(ErrorMessage = "Token is required")]
        public string? Token { get; set; }

    }
}

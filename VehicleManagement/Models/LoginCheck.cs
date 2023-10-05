using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class LoginCheck
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}

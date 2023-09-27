using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class LoginCheck
    {
        [Key]
        public int Id { get; set; } 

        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}

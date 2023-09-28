using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class ValidateUserscs
    {
        [Key]
        public Nullable<int> VUserid { get; set; }
        public string? Roles { get; set; }

    }
}

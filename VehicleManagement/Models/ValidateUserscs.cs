using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class ValidateUserscs
    {
        [Key]
        public Nullable<int> VUserid { get; set; }
        //[Required(ErrorMessage = "Roles is required")]
        public string? Roles { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class EmailModel
    {

        //[Required(ErrorMessage = "From address is required")]
        public string? From { get; set; }
        //[Required(ErrorMessage = "To address is required")]
        public string? To { get; set; }
        //[Required(ErrorMessage = "UserId is required")]
        public int Vuserid { get; set; }
    }
}

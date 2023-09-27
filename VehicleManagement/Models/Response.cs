using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VehicleManagement.Models
{
    internal class Response : ModelStateDictionary
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
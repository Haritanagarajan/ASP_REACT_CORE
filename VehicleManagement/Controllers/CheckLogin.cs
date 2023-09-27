using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;
using NuGet.Protocol.Plugins;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckLogin : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public CheckLogin(VehicleManagementContext context)
        {
            _context = context;
        }


        //[HttpPost]
        //[Route("Users")]
        //public IActionResult EmployeeLogin(LoginCheck login)
        //{
        //    var log = _context.Vusers.FirstOrDefault(x => x.Vusername == login.UserName && x.Vpassword == login.Password);

        //    if (log == null)
        //    {
        //        return BadRequest(new Models.Response { Status = "Invalid", Message = "Invalid User." });
        //    }
        //    else
        //    {
        //        return Ok(new Models.Response { Status = "Success", Message = "Login Successfully" });
        //    }
        //}


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Newtonsoft.Json;
using Octokit;
using VehicleManagement.Models;
namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VehicleManagementContext _context;
        public UsersController(VehicleManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// list all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Vuser>>> GetVusers()
        {
            if (_context.Vusers == null)
            {
                return NotFound();
            }
            return await _context.Vusers.ToListAsync();
        }
        /// <summary>
        /// fetching users based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Vuser>> GetVuser(int id)
        {
            if (_context.Vusers == null)
            {
                return NotFound();
            }
            var vuser = await _context.Vusers.FindAsync(id);
            if (vuser == null)
            {
                return NotFound();
            }
            return vuser;
        }
        /// <summary>
        /// editing the user based on id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vuser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PutVuser(int id, Vuser vuser)
        {
            if (id != vuser.Vuserid)
            {
                return BadRequest();
            }
            _context.Entry(vuser).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!VuserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        /// <summary>
        /// creating user
        /// </summary>
        /// <param name="vuser"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Vuser>> PostVuser(Vuser vuser)
        {
            vuser.Vcreated = DateTime.Now;
            vuser.VlastLoginDate = DateTime.Now;
            _context.Vusers.Add(vuser);

            try
            {
                await _context.SaveChangesAsync();
                var response = new Models.Vuser
                {
                    Vroleid = vuser.Vroleid,
                    Vusername = vuser.Vusername,
                    Vpassword = vuser.Vpassword,
                    Vuserid = vuser.Vuserid,
                    VlastLoginDate = vuser.VlastLoginDate,
                    Vcreated = vuser.Vcreated,
                    Vemail = vuser.Vemail,
                    Vmobile = vuser.Vmobile,
                };
                return Ok(response);
            }
            catch (Exception)
            {
                BadRequest("Error in Posting the details");
            }
            return Ok();
        }
        /// <summary>
        /// deleting user based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVuser(int id)
        {
            if (_context.Vusers == null)
            {
                return NotFound();
            }
            var vuser = await _context.Vusers.FindAsync(id);
            if (vuser == null)
            {
                return NotFound();
            }
            _context.Vusers.Remove(vuser);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// checks whether users record exists or not return bool value true/false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool VuserExists(int id)
        {
            return (_context.Vusers?.Any(e => e.Vuserid == id)).GetValueOrDefault(); 
        }
        /// <summary>
        /// Procedure to Decrypt and returns userid and rolename
        /// </summary>
        /// <param name="validate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ValidateUserscs>>> ValidateUser([FromBody] ValidateCheck validate)
        {
            var result = await _context.ValidateUserscs
                .FromSqlRaw("[dbo].[Validate_Users] @Username, @Password",
                    new SqlParameter("Username", validate.vusername),
                    new SqlParameter("Password", validate.vpassword))
                .ToListAsync();

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                var response = new Models.ValidateUserscs
                {
                    VUserid = result[0].VUserid,
                    Roles = result[0].Roles,
                };

                var tokenResult = TokenGenerate(response);

                if (tokenResult is OkObjectResult okObjectResult)
                {
                    string jwt = okObjectResult.Value?.ToString();

                    var Logindetails = new Models.LoginTokenDetails
                    {
                        VUserid = (int)result[0].VUserid,
                        Roles = result[0].Roles,
                        tokenResult = jwt
                    };
                    return Ok(Logindetails);
                }
                return Ok();

            }
        }
        /// <summary>
        /// generates token base on the Roles(rolenames) by passing ValidateUserscs values
        /// </summary>
        /// <param name="jwtcheck"></param>
        /// <returns></returns>
        [HttpPost("getToken")]
        public IActionResult TokenGenerate([FromBody] ValidateUserscs user)
        {
            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs0bn";
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
              {
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim(ClaimTypes.Role, user.Roles),
              };
            var token = new JwtSecurityToken(
                issuer: "JWTAuthenticationServer",
                audience: "JWTServicePostmanClient",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwtToken);
        }
    }
}


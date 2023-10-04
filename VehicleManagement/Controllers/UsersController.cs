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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vuser>>> GetVusers()
        {
            if (_context.Vusers == null)
            {
                return NotFound();
            }
            return await _context.Vusers.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vuser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
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
            catch (DbUpdateConcurrencyException)
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
        /// 
        /// </summary>
        /// <param name="vuser"></param>
        /// <returns></returns>
        [HttpPost]
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
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

        private bool VuserExists(int id)
        {
            return (_context.Vusers?.Any(e => e.Vuserid == id)).GetValueOrDefault();
        }


        /// <summary>
        /// Procedure to Decrypt
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
                    Roles = result[0].Roles
                };
                return Ok(response);

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtcheck"></param>
        /// <returns></returns>
        [HttpPost("getToken")]
        public IActionResult TokenGenerate([FromBody] JwtCheck jwtcheck)
        {
            var log = _context.Vusers.FirstOrDefault(x => x.Vemail == jwtcheck.Email);

            if (log == null)
            {
                return BadRequest(new Models.Response { Status = "Invalid", Message = "Invalid User." });
            }

            else
            {
                int? roleid = log.Vroleid;

                var Roles = "no role";

                if (roleid == 1)
                {
                    Roles = "Admin";
                }
                if (roleid == 2)
                {
                    Roles = "Customer";
                }

                if (roleid == null)
                {
                    return BadRequest("User does not have a role.");
                }

                var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs0bn";
                var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
              {
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim(ClaimTypes.Role, Roles),
              };

                var token = new JwtSecurityToken(
                    issuer: "JWTAuthenticationServer",
                    audience: "JWTServicePostmanClient",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var response = new Models.JwtCheck
                {
                    Email = jwtcheck.Email,
                    Token = jwtToken
                };

                return Ok(response);
            }
        }
    }
}

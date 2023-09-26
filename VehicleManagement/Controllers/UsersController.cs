using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public UsersController(VehicleManagementContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vuser>>> GetVusers()
        {
          if (_context.Vusers == null)
          {
              return NotFound();
          }
            return await _context.Vusers.ToListAsync();
        }

        // GET: api/Users/5
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

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vuser>> PostVuser(Vuser vuser)
        {
          if (_context.Vusers == null)
          {
              return Problem("Entity set 'VehicleManagementContext.Vusers'  is null.");
          }
            _context.Vusers.Add(vuser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVuser", new { id = vuser.Vuserid }, vuser);
        }

        // DELETE: api/Users/5
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


        [HttpPost("validate")]
       
        public async Task<ActionResult<IEnumerable<ValidateUserscs>>> ValidateUser(string username,string password)
        {
            var result = await _context.ValidateUserscs
                .FromSqlRaw("[dbo].[Validate_Users] @Username, @Password",
                    new SqlParameter("Username", username),
                    new SqlParameter("Password", password))
                .ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;

        }

    }
}

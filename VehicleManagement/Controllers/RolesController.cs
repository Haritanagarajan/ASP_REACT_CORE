using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly VehicleManagementContext _context;
        public RolesController(VehicleManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vrole>>> GetVroles()
        {
            if (_context.Vroles == null)
            {
                return NotFound();
            }
            return await _context.Vroles.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Vrole>> GetVrole(int id)
        {
            if (_context.Vroles == null)
            {
                return NotFound();
            }
            var vrole = await _context.Vroles.FindAsync(id);
            if (vrole == null)
            {
                return NotFound();
            }
            return vrole;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vrole"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVrole(int id, Vrole vrole)
        {
            if (id != vrole.Vroleid)
            {
                return BadRequest();
            }
            _context.Entry(vrole).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VroleExists(id))
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
        /// <param name="vrole"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Vrole>> PostVrole(Vrole vrole)
        {
            if (_context.Vroles == null)
            {
                return Problem("Entity set 'VehicleManagementContext.Vroles'  is null.");
            }
            _context.Vroles.Add(vrole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VroleExists(vrole.Vroleid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetVrole", new { id = vrole.Vroleid }, vrole);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVrole(int id)
        {
            if (_context.Vroles == null)
            {
                return NotFound();
            }
            var vrole = await _context.Vroles.FindAsync(id);
            if (vrole == null)
            {
                return NotFound();
            }
            _context.Vroles.Remove(vrole);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool VroleExists(int id)
        {
            return (_context.Vroles?.Any(e => e.Vroleid == id)).GetValueOrDefault();
        }
    }
}

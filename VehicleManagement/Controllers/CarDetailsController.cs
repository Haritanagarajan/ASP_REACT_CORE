using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private readonly VehicleManagementContext _context;

        public CarDetailsController(VehicleManagementContext context)
        {
            _context = context;
        }

        // GET: api/CarDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDetail>>> GetCarDetails()
        {
          if (_context.CarDetails == null)
          {
              return NotFound();
          }
            return await _context.CarDetails.ToListAsync();
        }

        // GET: api/CarDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDetail>> GetCarDetail(int id)
        {
          if (_context.CarDetails == null)
          {
              return NotFound();
          }
            var carDetail = await _context.CarDetails.FindAsync(id);

            if (carDetail == null)
            {
                return NotFound();
            }

            return carDetail;
        }

        // PUT: api/CarDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarDetail(int id, CarDetail carDetail)
        {
            if (id != carDetail.DetailsId)
            {
                return BadRequest();
            }

            _context.Entry(carDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarDetailExists(id))
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

        // POST: api/CarDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarDetail>> PostCarDetail(CarDetail carDetail)
        {
          if (_context.CarDetails == null)
          {
              return Problem("Entity set 'VehicleManagementContext.CarDetails'  is null.");
          }
            _context.CarDetails.Add(carDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarDetail", new { id = carDetail.DetailsId }, carDetail);
        }

        // DELETE: api/CarDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarDetail(int id)
        {
            if (_context.CarDetails == null)
            {
                return NotFound();
            }
            var carDetail = await _context.CarDetails.FindAsync(id);
            if (carDetail == null)
            {
                return NotFound();
            }

            _context.CarDetails.Remove(carDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarDetailExists(int id)
        {
            return (_context.CarDetails?.Any(e => e.DetailsId == id)).GetValueOrDefault();
        }

        [HttpPost]

        public ActionResult Mailer(int Vuserid)
        {

            var order = _context.CarDetails.FirstOrDefault(o => o.Vuserid == Vuserid);

            if (order == null)
            {
                return BadRequest();
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("VIGNESH", "20bsca150vigneshr@skacas.ac.in"));
            message.To.Add(new MailboxAddress(order.VuserName,order.Vuser.Vemail));
            message.Subject = "Order Confirmation";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = "Thank you for booking  the Car Service with us:\n\n" +
                            $"User Name: {order.VuserName}\n" +
                            $"Order Id: {order.DetailsId}\n" +
                            $"Due Date: {order.DueDate}"
            };


            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("20bsca150vigneshr@skacas.ac.in", "welcome123");
                client.Send(message);
                client.Disconnect(true);
            }


            return RedirectToAction("Create", "Feedback");

        }
    }
}

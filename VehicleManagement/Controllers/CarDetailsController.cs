using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Octokit;
using VehicleManagement.Interface;
using VehicleManagement.Models;
using VehicleManagement.Repository;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CarDetailsController : ControllerBase
    {
        private readonly ICarDetails _detailsrepo;
        private readonly VehicleManagementContext _context;

        public CarDetailsController(ICarDetails detailsrepo, VehicleManagementContext context)
        {
            _detailsrepo = detailsrepo;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carDetail"></param>
        /// <returns></returns>
        [HttpPost("Details")]
        [Authorize(Roles="Customer")]
        public async Task<ActionResult<CarDetail>> PostCarDetail(CarDetail carDetail)
        {
            return await _detailsrepo.PostCarDetail(carDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult Mailer(EmailModel email)
        {

            var order = _context.CarDetails.FirstOrDefault(o => o.Vuserid == email.Vuserid);
            if (order == null)
            {
                return BadRequest();
            }
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(email.From));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = "Order Confirmation";
            var bodyBuilder = new BodyBuilder
            {
                TextBody = "Thank you for booking the Car Service with us:\n\n" +
                "Yor Payment has been successfully done:\n\n" + 
                            $"User Name: {order.VuserName}\n" +
                            $"Order Id: {order.DetailsId}\n" +
                            $"Due Date: {order.DueDate}\n" +
                            $"BrandName: {order.Brandid}\n" +
                            $"Service Name: {order.Serviceid}\n" +
                            $"BrandImage: {order.BrandImage}\n" +
                            "For any queries? contact us 6382830515\n\n" 


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

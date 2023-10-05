using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
            private RazorpayClient _razorpayClient;

            public PaymentController()
            {
                _razorpayClient = new RazorpayClient("rzp_test_D3KXHgdS7fmKuO",
                    "GYl4qNswv7eZvy5RMzSoFen3");
            }

            [HttpPost]
            [Route("initialize")]
            public async Task<IActionResult> InitializePayment(int amount)
            {
                var options = new Dictionary<string, object>
            {
                { "amount", amount * 100},
                { "currency", "INR" },
                { "receipt", "recipt_1" },
                { "payment_capture", true }
            };

                var order = _razorpayClient.Order.Create(options);
                var orderId = order["id"].ToString();
                var orderJson = order.Attributes.ToString();
                return Ok(orderJson);
            }

            public class ConfirmPaymentPayload
            {
                public string razorpay_payment_id { get; }
                public string razorpay_order_id { get; }
                public string razorpay_signature { get; }
            }

            [HttpPost]
            [Route("confirm")]
            public async Task<IActionResult> ConfirmPayment(ConfirmPaymentPayload confirmPayment)
            {
                var attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", confirmPayment.razorpay_payment_id },
                { "razorpay_order_id", confirmPayment.razorpay_order_id },
                { "razorpay_signature", confirmPayment.razorpay_signature }
            };
                try
                {
                    Utils.verifyPaymentSignature(attributes);
                    
                    var isValid = Utils.ValidatePaymentSignature(attributes);
                    if (isValid)
                    {
                        var order = _razorpayClient.Order.Fetch(confirmPayment.razorpay_order_id);
                        var payment = _razorpayClient.Payment.Fetch(confirmPayment.razorpay_payment_id);
                        if (payment["status"] == "captured")
                        {
                            return Ok("Payment Successful");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }



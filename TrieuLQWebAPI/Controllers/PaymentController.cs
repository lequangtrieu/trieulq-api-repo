using Microsoft.AspNetCore.Mvc;
using TrieuLQWebAPI.DTO;
using TrieuLQWebAPI.Service;

namespace TrieuLQWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly PaymentService _paymentService;

		public PaymentController(PaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpPost("payment-link")]
		public async Task<IActionResult> CreatePaymentLink([FromBody] CreatePaymentLinkRequest request)
		{
			try
			{
				// Chờ đợi hoàn thành trước khi trả về
				var paymentLink = await _paymentService.CreatePaymentLink(request);
				return Ok(new { PaymentLink = paymentLink });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}


		[HttpGet("paymentSuccess")]
		public async Task<IActionResult> PaymentSuccess([FromQuery] PaymentSuccessRequest request)
		{
			// If payment is successful: code = "00" and status = "PAID"
			if (request.Code == "00" && request.Status.Equals("PAID", StringComparison.OrdinalIgnoreCase))
			{
				return Ok(new { Status = '1' });
			}
			return Ok(new { Status = '1' });
		}
	}
}

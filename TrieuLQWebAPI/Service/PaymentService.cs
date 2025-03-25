using Net.payOS;
using Net.payOS.Types;
using TrieuLQWebAPI.DTO;

namespace TrieuLQWebAPI.Service
{
	public class PaymentService
	{
		private readonly PayOS _payOS;

		public PaymentService(PayOS payOs)
		{
			_payOS = payOs;
		}

		public async Task<string> CreatePaymentLink(CreatePaymentLinkRequest request)
		{
			// Chuẩn bị dữ liệu gửi yêu cầu tạo link thanh toán
			ItemData item = new ItemData(request.OrderName, 1, (int)request.TotalPrice);
			List<ItemData> items = new List<ItemData> { item };
			PaymentData paymentData = new PaymentData(request.OrderId, (int)request.TotalPrice, request.OrderName, items, request.cancelUrl, request.returnUrl);

			// Gửi yêu cầu tạo link thanh toán
			CreatePaymentResult response = await _payOS.createPaymentLink(paymentData);

			if (response == null || string.IsNullOrEmpty(response.checkoutUrl))
			{
				throw new Exception("Failed to create payment link");
			}

			// Trả về URL thanh toán
			return response.checkoutUrl;
		}
	}
}

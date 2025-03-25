namespace TrieuLQWebAPI.DTO
{
	public class CreatePaymentLinkRequest
	{
		public int OrderId { get; set; }
		public string OrderName { get; set; }
		public double TotalPrice { get; set; }
		public string returnUrl { get; set; } = "https://localhost:7091/api/Payment/paymentSuccess";
		public string cancelUrl { get; set; } = "https://localhost:7091/api/Payment/paymentSuccess";
	}
}

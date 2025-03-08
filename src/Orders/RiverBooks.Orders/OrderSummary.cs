namespace RiverBooks.Orders
{
	public class OrderSummary
	{
		public DateTimeOffset CreatedDate { get; set; }
		public decimal Total { get; set; }
		public Guid UserId { get; set; }
		public Guid OrderId { get; set; }
	}
}
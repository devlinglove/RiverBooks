using System.Net;

namespace RiverBooks.Orders
{
	internal class Order
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Address ShippingAddress { get; set; }
		public Address BillingAddress { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		private readonly List<OrderItem> _orderItems = new();
		public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly(); 

	}
}

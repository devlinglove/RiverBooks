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

		private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

		internal class Factory
		{
			public static Order Create(Guid userId,
			  Address shippingAddress,
			  Address billingAddress,
			  IEnumerable<OrderItem> orderItems)
			{
				var order = new Order();
				order.UserId = userId;
				order.ShippingAddress = shippingAddress;
				order.BillingAddress = billingAddress;
				foreach (var item in orderItems)
				{
					order.AddOrderItem(item);
				}

				//var createdEvent = new OrderCreatedEvent(order);
				//order.RegisterDomainEvent(createdEvent);

				return order;
			}
		}

	}

}

	 
	

using MediatR;
using RiverBooks.Orders.Contracts;
using Ardalis.Result;


namespace RiverBooks.Orders.Integrations
{
	internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
	{
		private readonly IOrderRepository _orderRepository;

		public CreateOrderCommandHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}
		public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var orderItems = request.OrderItems.Select(oi => new OrderItem(oi.BookId, oi.Quantity, oi.UnitPrice, oi.Description));

			var shippingAddress = new Address("123 Main", "", "Kent", "OH", "44444", "USA");
			var billingAddress = shippingAddress;

			var newOrder = Order.Factory.Create(request.UserId, shippingAddress, billingAddress, orderItems);


			await _orderRepository.AddAsync(newOrder);
			await _orderRepository.SaveChangesAsync();

			return new OrderDetailsResponse(newOrder.Id);

		}
	}
}

using MediatR;
using RiverBooks.Orders.Contracts;
using Ardalis.Result;
using RiverBooks.Orders.Domain;
using RiverBooks.Orders.Interfaces;


namespace RiverBooks.Orders.Integrations
{
	internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderAddressCache _orderAddressCache;

		public CreateOrderCommandHandler(
			IOrderRepository orderRepository,
			IOrderAddressCache orderAddressCache
		)
		{
			_orderRepository = orderRepository;
			_orderAddressCache = orderAddressCache;
		}
		public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var orderItems = request.OrderItems.Select(oi => new OrderItem(oi.BookId, oi.Quantity, oi.UnitPrice, oi.Description));

			//var shippingAddress = new Address("123 Main", "", "Kent", "OH", "44444", "USA");
			//var billingAddress = shippingAddress;

			var shippingAddress = await _orderAddressCache.GetByIdAsync(request.ShippingAddressId);
			var billingAddress = await _orderAddressCache.GetByIdAsync(request.ShippingAddressId);

			var newOrder = Order.Factory.Create(
				request.UserId, 
				shippingAddress.Value.Address, 
				billingAddress.Value.Address, 
				orderItems
			);


			await _orderRepository.AddAsync(newOrder);
			await _orderRepository.SaveChangesAsync();

			return new OrderDetailsResponse(newOrder.Id);

		}
	}
}

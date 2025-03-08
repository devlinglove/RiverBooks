using MediatR;
using Ardalis.Result;
using RiverBooks.Orders.Interfaces;


namespace RiverBooks.Orders.UseCases.Orders.ListOrders
{
	internal record ListOrdersForUserQuery(string emailAddress) : IRequest<Result<List<OrderSummary>>>;

	internal class ListOrdersForUserQueryHandler : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
	{
		private readonly IOrderRepository _orderRepository;

		public ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}
		public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
		{
			var orders = await _orderRepository.ListOrderAsync();
			return orders.Select(o => new OrderSummary
			{
				OrderId = o.Id,
				CreatedDate = o.DateCreated,
				UserId = o.UserId,
				Total = o.OrderItems.Sum(oi => oi.UnitPrice)
			})
			.ToList();

		}
	}




}



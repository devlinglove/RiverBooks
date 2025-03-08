using MediatR;
using Ardalis.Result;

namespace RiverBooks.Orders.Contracts
{
	public record CreateOrderCommand(Guid UserId,
								 Guid ShippingAddressId,
								 Guid BillingAddressId,
								 List<OrderItemDetails> OrderItems) :
	IRequest<Result<OrderDetailsResponse>>;
}

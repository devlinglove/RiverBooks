using MediatR;
using Ardalis.Result;

namespace RiverBooks.Users.UseCases.Cart.AddItem
{
	public record AddItemToCartCommand(Guid bookId, int Quantity, string emailAddress) : IRequest<Result>;

}

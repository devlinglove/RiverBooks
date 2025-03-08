using Ardalis.Result;
using MediatR;
using RiverBooks.Users.DTOs;

namespace RiverBooks.Users.UseCases.Cart.ListItems
{
	public record ListCartItemsQuery(string emailAddress) : IRequest<Result<List<CartItemDto>>>;
}

using MediatR;
using Ardalis.Result;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Commands
{
	public record AddItemToCartCommand(Guid bookId, int Quantity, string emailAddress):IRequest<Result>;


	public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
	{
		private readonly IApplicationUserRepository _applicationUserRepo;
		private readonly ISender _sender;

		public AddItemToCartHandler(
			IApplicationUserRepository applicationRepo,
			ISender sender
		)
		{
			_applicationUserRepo = applicationRepo;
			_sender = sender;
		}
		public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
		{
			var user = await _applicationUserRepo.GetUserWithCartByEmailAsync(request.emailAddress);
			if (user == null) {
				return Result.Unauthorized();
			}

			var query = new BooksDetailQuery(request.bookId);
			var result = await _sender.Send(query);

			if (!result.IsSuccess)
			{
				return Result.NotFound();
			}

			var book = result.Value;
			string description = $"{book.Title} by {book.Author}";

			var newItem = new CartItem(request.bookId, description, request.Quantity, book.UnitPrice);

			user.AddToCart(newItem);

			await _applicationUserRepo.SaveChangesAsync();

			return Result.Success();

		}
	}


	

}

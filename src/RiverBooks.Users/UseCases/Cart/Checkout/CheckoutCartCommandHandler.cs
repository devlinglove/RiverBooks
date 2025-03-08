using Ardalis.Result;
using MediatR;
using RiverBooks.Orders.Contracts;

namespace RiverBooks.Users.UseCases.Cart.Checkout
{
	public class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Result<Guid>>
	{
		private readonly IApplicationUserRepository _applicationUserRepository;
		private readonly ISender _sender;

		public CheckoutCartCommandHandler(
			IApplicationUserRepository applicationUserRepository,
			ISender sender
			)
		{
			_applicationUserRepository = applicationUserRepository;
			_sender = sender;
		}
		public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
		{
			var user = await _applicationUserRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
			if (user == null)
			{
				return Result.Unauthorized();
			}

			var items = user.CartItems.Select(i => new OrderItemDetails
			(i.BookId, i.Quantity, i.UnitPrice, i.Description)).ToList();

			var orderCommand = new CreateOrderCommand(Guid.Parse(user.Id), request.shippingAddressId, request.billingAddressId, items);
			var result = await _sender.Send(orderCommand);

			if (!result.IsSuccess)
			{
				return result.Map(x => x.OrderId);
			}

			user.ClearCart();
			await _applicationUserRepository.SaveChangesAsync();

			return Result.Success(result.Value.OrderId);


		}
	}

}

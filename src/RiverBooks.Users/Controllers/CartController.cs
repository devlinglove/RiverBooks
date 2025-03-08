using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiverBooks.Users.DTOs;
using RiverBooks.Users.UseCases.Cart.AddItem;
using RiverBooks.Users.UseCases.Cart.Checkout;
using RiverBooks.Users.UseCases.Cart.ListItems;
using System.Security.Claims;


namespace RiverBooks.Users.Controllers
{
	//[Route("api/[controller]")]
	//[ApiController]
	public class CartController:BaseApiController
	{
		//private readonly ISender _sender;
		//private readonly IMediator _mediator;

		public CartController(ISender sender) : base(sender)
		{
			
		}

		[Authorize]
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] AddCartItemDto cartDto)
		{
			var emailAddress = User.FindFirstValue(ClaimTypes.Email);
			var command = new AddItemToCartCommand(cartDto.BookId, cartDto.Quantity, emailAddress);
			var result = await _sender.Send(command);

			if (result.Status == ResultStatus.Unauthorized)
			{
				return Unauthorized();
			}

			return Ok();
		}

		[Authorize]
		[HttpGet("list")]
		public async Task<ActionResult<List<CartItemDto>>> ListCartItems()
		{
			var emailAddress = User.FindFirstValue(ClaimTypes.Email);
			var query = new ListCartItemsQuery(emailAddress);
			var result = await _sender.Send(query);


			if (result.Status == ResultStatus.Unauthorized)
			{
				return Unauthorized();
			}

			return Ok(result.Value);

		}
		[Authorize]
		[HttpPost("checkout")]
		public async Task<ActionResult<Result<Guid>>> Checkout(CheckoutRequest request)
		{
			var emailAddress = User.FindFirstValue(ClaimTypes.Email);
			var checkoutCommand = new CheckoutCartCommand(emailAddress, request.shippingAddressId, request.billingAddressId);

			var result = await _sender.Send(checkoutCommand);
			if (result.Status == ResultStatus.Unauthorized)
			{
				return Unauthorized();
			}

			return Ok(result.Value);
		}

	}
}

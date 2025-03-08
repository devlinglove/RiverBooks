

using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RiverBooks.Orders.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController:ControllerBase
	{
		private readonly ISender _sender;

		public OrdersController(ISender sender)
		{
			_sender = sender;
		}
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<List<OrderSummary>>> ListOrders() 
		{
			var emailAddress = User.FindFirstValue(ClaimTypes.Email);
			var orderQuery = new ListOrdersForUserQuery(emailAddress);
			var result = await _sender.Send(orderQuery);
			if(result.Status == ResultStatus.Unauthorized)
			{
				return Unauthorized();
			}
			return Ok(result.Value);
		}
	}
}

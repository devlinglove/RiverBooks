using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiverBooks.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Controllers
{
	public class AddressController:BaseApiController
	{
		public AddressController(ISender sender) : base(sender)
		{
			
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateUserAddress(AddAddressRequest request)
		{
			var emailAddress = User.FindFirstValue(ClaimTypes.Email);
			var command = new AddAddressToUserCommand(emailAddress!,
				  request.Street1,
				  request.Street2,
				  request.City,
				  request.State,
				  request.PostalCode,
				  request.Country);

			var result = await _sender.Send(command);

			if (result.Status == ResultStatus.Unauthorized)
			{
				return Unauthorized();
			}

			return Ok();
		}

	}
}

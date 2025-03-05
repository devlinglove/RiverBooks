using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RiverBooks.Users.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseApiController : ControllerBase
	{
		protected readonly ISender _sender;

		protected BaseApiController(ISender sender)
		{
			_sender = sender;
		}
	}
}

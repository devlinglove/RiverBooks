using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.Controllers
{
	internal class ListAddressesQueryHandler : IRequestHandler<ListAddressesQuery, Result<List<UserAddressDto>>>
	{
		public Task<Result<List<UserAddressDto>>> Handle(ListAddressesQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}

}


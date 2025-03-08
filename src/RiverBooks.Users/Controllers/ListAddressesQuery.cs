using Ardalis.Result;
using MediatR;


namespace RiverBooks.Users.Controllers
{
	internal record ListAddressesQuery(string EmailAddress) :
  IRequest<Result<List<UserAddressDto>>>;

}


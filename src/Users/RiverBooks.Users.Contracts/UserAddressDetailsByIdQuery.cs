using Ardalis.Result;
using MediatR;


namespace RiverBooks.Users.Contracts
{
	public record UserAddressDetailsByIdQuery(Guid addressId) : IRequest<Result<UserAddressDetails>>;
	
}


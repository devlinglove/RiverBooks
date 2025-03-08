using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.Interfaces;


namespace RiverBooks.Users.Integration
{
	internal class UserAddressDetailsByIdQueryHandler : IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
	{
		private readonly IAddressRepository _addressRepository;

		public UserAddressDetailsByIdQueryHandler(IAddressRepository addressRepository)
		{
			_addressRepository = addressRepository;
		}
		public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request, CancellationToken cancellationToken)
		{
			var address = await _addressRepository.GetAddressByIdAsync(request.addressId);
			if(address == null)
			{
				return Result.NotFound();
			}

			Guid userId = Guid.Parse(address.UserId);

			var details = new UserAddressDetails(userId,
				  address.Id,
				  address.StreetAddress.Street1,
				  address.StreetAddress.Street2,
				  address.StreetAddress.City,
				  address.StreetAddress.State,
				  address.StreetAddress.PostalCode,
				  address.StreetAddress.Country);

			return details;
		}
	}
}

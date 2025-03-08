﻿

using MediatR;
using Ardalis.Result;
using RiverBooks.Users.Commands;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Commands
{
	public record AddAddressToUserCommand(string EmailAddress,
					  string Street1,
					  string Street2,
					  string City,
					  string State,
					  string PostalCode,
					  string Country) : IRequest<Result>;
}

public class AddAddressToUserCommandHandler : IRequestHandler<AddAddressToUserCommand, Result>
{
	private readonly IApplicationUserRepository _applicationUserRepository;

	public AddAddressToUserCommandHandler(IApplicationUserRepository applicationUserRepository)
	{
		_applicationUserRepository = applicationUserRepository;
	}
	public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _applicationUserRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);

		if(user == null)
		{
			return Result.Unauthorized();
		}

		var addressToAdd = new Address(request.Street1,
								   request.Street2,
								   request.City,
								   request.State,
								   request.PostalCode,
								   request.Country);

		user.AddAddress(addressToAdd);

		await _applicationUserRepository.SaveChangesAsync();

		return Result.Success();

	}
}

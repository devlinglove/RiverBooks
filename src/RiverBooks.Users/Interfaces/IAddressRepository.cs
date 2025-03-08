using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

internal interface IAddressRepository
{
	Task<UserStreetAddress> GetAddressByIdAsync(Guid id);
}

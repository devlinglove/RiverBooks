using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

internal class EfUserStreetAddressRepository : IAddressRepository
{
	private readonly UserDbContext _userDbContext;

	public EfUserStreetAddressRepository(UserDbContext userDbContext)
	{
		_userDbContext = userDbContext;
	}
	public async Task<UserStreetAddress> GetAddressByIdAsync(Guid id)
	{
		return await _userDbContext.UserStreetAddresses.FindAsync(id);
	}
}
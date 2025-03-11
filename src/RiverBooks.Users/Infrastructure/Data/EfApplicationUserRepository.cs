using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;


namespace RiverBooks.Users.Infrastructure.Data
{
	public class EfApplicationUserRepository : IApplicationUserRepository
	{
		private readonly UserDbContext _userDbContext;

		public EfApplicationUserRepository(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		//public async Task<ApplicationUser> GetApplicationUserById(string email)
		//{
		//	return await _userDbContext.ApplicationUsers.Include(user => user.CartItems).SingleAsync(x => x.Email == email);
		//}

		public Task<ApplicationUser> GetUserByIdAsync(Guid userId)
		{
			return _userDbContext.ApplicationUsers
			  .SingleAsync(user => user.Id == userId.ToString());
		}

		public Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
		{
			return _userDbContext.ApplicationUsers
			  //.Include(user => user.Addresses)
			  .SingleAsync(user => user.Email == email);

		}

		public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
		{
			return _userDbContext.ApplicationUsers
			  .Include(user => user.CartItems)
			  .SingleOrDefaultAsync(user => user.Email == email);
		}

		public Task SaveChangesAsync()
		{
			return _userDbContext.SaveChangesAsync();
		}
	}
}

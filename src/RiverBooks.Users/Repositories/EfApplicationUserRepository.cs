using Microsoft.EntityFrameworkCore;


namespace RiverBooks.Users.Repositories
{
	public class EfApplicationUserRepository : IApplicationUserRepository
	{
		private readonly UserDbContext _userDbContext;

		public EfApplicationUserRepository(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}
		public async Task<ApplicationUser> GetApplicationUserById(string email)
		{
			return await _userDbContext.ApplicationUsers.Include(user => user.CartItems).SingleAsync(x => x.Email == email);
		}

		public async Task SaveChangesAsync()
		{
			await _userDbContext.SaveChangesAsync();
		}
	}
}

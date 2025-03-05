using RiverBooks.Users;

public interface IApplicationUserRepository
{
	Task<ApplicationUser> GetApplicationUserById(string email);
	Task SaveChangesAsync();
}

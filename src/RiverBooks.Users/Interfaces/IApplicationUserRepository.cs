using RiverBooks.Users.Domain;

public interface IApplicationUserRepository
{
	//Task<ApplicationUser> GetApplicationUserById(string email);
	Task<ApplicationUser> GetUserByIdAsync(Guid userId);
	Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
	Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
	Task SaveChangesAsync();
}

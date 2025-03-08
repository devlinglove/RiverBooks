using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

public interface ITokenService
{
	string CreateToken(ApplicationUser user, IList<string> roles);
}

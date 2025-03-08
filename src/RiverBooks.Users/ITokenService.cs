using RiverBooks.Users.Domain;

namespace RiverBooks.Users;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, IList<string> roles);
}

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RiverBooks.Users.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RiverBooks.Users
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _jwtKey;

		public TokenService(IConfiguration config)
		{
			_config = config;
			_jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
		}

		public string CreateToken(ApplicationUser user, IList<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.UserName)
			};

			// Add role claims
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}



			//SecurityAlgorithms.HmacSha512Signature
			var creds = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256Signature);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresInMinutes"])),
				SigningCredentials = creds,
				Issuer = _config["Jwt:Issuer"],
				Audience = _config["Jwt:Audience"]
			};


			var tokenHandler = new JwtSecurityTokenHandler();
			var jwt = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(jwt);


		}
	}
}

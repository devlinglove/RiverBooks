namespace RiverBooks.Users.DTOs
{
	public class UserDto
	{
        public string Email { get; set; }
		public string Token { get; set; }

		public IList<string> Roles { get; set; } = new List<string>();
		
	}
}

namespace RiverBooks.Users.DTOs
{
	public class AddCartItemDto
	{
		public Guid BookId { get; set; }
		public int Quantity { get; set; }
	}
}

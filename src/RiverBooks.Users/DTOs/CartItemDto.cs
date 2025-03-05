

namespace RiverBooks.Users.DTOs
{
	public class CartItemDto
	{
		public Guid BookId { get; set; }
		public int Quantity { get; set; }

		public string Description { get; set; }

	}
}

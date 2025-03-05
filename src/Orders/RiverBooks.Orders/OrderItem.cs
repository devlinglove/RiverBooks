namespace RiverBooks.Orders
{
	internal class OrderItem
	{
		public Guid Id { get; set; }
		public Guid BookId { get; private set; }
		public string Description { get; private set; }
		public int Quantity { get; private set; }
		public decimal UnitPrice { get; private set; }
	}
}

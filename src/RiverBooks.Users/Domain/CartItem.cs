namespace RiverBooks.Users.Domain;

public class CartItem
{
	public Guid Id { get; private set; }
	public Guid BookId { get; private set; }
	public string Description { get; private set; }
	public int Quantity { get; private set; }
	public decimal UnitPrice { get; private set; }

	public CartItem()
	{

	}

	public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
	{

		BookId = bookId;
		Description = description;
		Quantity = quantity;
		UnitPrice = unitPrice;
	}

	public void AdjustQuantity(int quantity)
	{
		//Quantity = Guard.Against.Negative(quantity);
		Quantity = quantity;
	}

	public void AdjustUnitPrice(decimal unitPrice)
	{
		UnitPrice = unitPrice;
	}

	public void UpdateDescription(string newDescription)
	{
		//Description = Guard.Against.NullOrEmpty(newDescription);
		Description = newDescription;
	}


}

using Microsoft.AspNetCore.Identity;
using System;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser
{
	public string FullName { get; set; } = string.Empty;
	private readonly List<CartItem> _cartItems = new();
	public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

	public void AddToCart(CartItem item)
	{
		var existingItem = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);
		if(existingItem != null)
		{
			item.AdjustQuantity(existingItem.Quantity + item.Quantity);
			// TODO: if other things have been updated
			item.UpdateDescription(item.Description);
			item.AdjustUnitPrice(item.UnitPrice);
			return;
		}

		_cartItems.Add(item);
	}


}

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

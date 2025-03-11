using Microsoft.AspNetCore.Identity;
using RiverBooks.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiverBooks.Users.Domain;

public class ApplicationUser : IdentityUser, IHaveDomainEvents
{
	public string FullName { get; set; } = string.Empty;
	private readonly List<CartItem> _cartItems = new();
	public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();
	private readonly List<UserStreetAddress> _addresses = new();
	public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

	private List<DomainEventBase> _domainEvents = new();
	[NotMapped]
	public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

	protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
	public void AddToCart(CartItem item)
	{
		var existingItem = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);
		if (existingItem != null)
		{
			existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);
			existingItem.UpdateDescription(item.Description);
			existingItem.UpdateUnitPrice(item.UnitPrice);
			return;
		}

		_cartItems.Add(item);
	}

	internal UserStreetAddress AddAddress(Address address)
	{
		//Guard.Against.Null(address);

		// find existing address and just return it
		var existingAddress = _addresses.SingleOrDefault(a => a.StreetAddress == address);
		if (existingAddress != null)
		{
			return existingAddress;
		}

		var newAddress = new UserStreetAddress(Id, address);
		_addresses.Add(newAddress);

		var addressEvent = new AddressAddedEvent(newAddress);
		RegisterDomainEvent(addressEvent);

		
		return newAddress;
	}

	internal void ClearCart()
	{
		_cartItems.Clear();
	}

	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}
}

﻿using RiverBooks.Users.Domain;

namespace RiverBooks.Users;

internal sealed class AddressAddedEvent : DomainEventBase
{
	public AddressAddedEvent(UserStreetAddress newAddress)
	{
		NewAddress = newAddress;
	}

	public UserStreetAddress NewAddress { get; }
}
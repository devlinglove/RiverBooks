﻿namespace RiverBooks.Users.Controllers
{
	public record UserAddressDto(Guid id,
					  string Street1,
					  string Street2,
					  string City,
					  string State,
					  string PostalCode,
					  string Country);
}

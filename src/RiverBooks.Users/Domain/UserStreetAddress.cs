﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Domain
{
	public class UserStreetAddress
	{
		public UserStreetAddress(string userId, Address streetAddress)
		{
			UserId = userId;
			StreetAddress = streetAddress;
		}

		private UserStreetAddress() { } // EF

		public Guid Id { get; private set; } = Guid.NewGuid();
		public string UserId { get; private set; } = string.Empty;
		public Address StreetAddress { get; private set; } = default!;
	}
}

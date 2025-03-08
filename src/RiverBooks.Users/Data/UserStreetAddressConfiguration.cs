using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Data
{
	public class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
	{
		public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
		{
			builder.ToTable(nameof(UserStreetAddress));
			builder.Property(x => x.Id).ValueGeneratedNever();
			builder.ComplexProperty(usa => usa.StreetAddress).IsRequired();
			builder.ComplexProperty(usa => usa.StreetAddress, address =>
			{
				address.Property(a => a.Street1).HasColumnName("Street1");
				address.Property(a => a.Street2).HasColumnName("Street2");
				address.Property(a => a.City).HasColumnName("City");
				address.Property(a => a.State).HasColumnName("State");
				address.Property(a => a.PostalCode).HasColumnName("Postal Code");
				address.Property(a => a.Country).HasColumnName("Country");

			});

			

		}
	}

}

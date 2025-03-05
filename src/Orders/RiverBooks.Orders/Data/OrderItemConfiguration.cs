using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Orders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
	{
		//Id column name could be anything in th database side.

		builder.Property(x => x.Id).HasColumnName("Id");
		builder.Property(x => x.Description)
			 .HasMaxLength(100)
			 .IsRequired();

	}
}
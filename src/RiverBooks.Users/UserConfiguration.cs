using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
  public void Configure(EntityTypeBuilder<ApplicationUser> builder)
  {
    
  }
}

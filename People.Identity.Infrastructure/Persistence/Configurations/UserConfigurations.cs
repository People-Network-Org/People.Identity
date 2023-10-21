using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    ConfigureUserTable(builder);
  }

  private void ConfigureUserTable(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");

    builder.HasKey(u => u.Id);

    builder.HasIndex(u => u.NickName).IsUnique();
    builder.HasIndex(u => u.Email).IsUnique();
    builder.HasIndex(u => u.Phone).IsUnique();

    builder.Property(u => u.Id)
      .ValueGeneratedNever()
      .HasConversion(id =>
        id.Value,
        value => UserId.Create(value));

    builder.Property(u => u.FirstName)
      .HasMaxLength(100);

    builder.Property(u => u.LastName)
      .HasMaxLength(100);

    builder.Property(u => u.NickName)
      .HasMaxLength(100);
  }
}
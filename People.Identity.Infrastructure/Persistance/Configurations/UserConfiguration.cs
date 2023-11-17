using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    ConfigureUserTable(builder);
    ConfigureUserRolesTable(builder);
    ConfigureUserClaimsTable(builder);
    ConfigureRefreshTokensTable(builder);
    ConfigureEmailCodeVO(builder);
  }

  private void ConfigureEmailCodeVO(EntityTypeBuilder<User> builder)
  {
    builder.OwnsOne(u => u.EmailCode, sb =>
    {
      sb.HasIndex(ec => ec.Code).IsUnique();

      sb.Property(ec => ec.Code)
        .IsRequired();

      sb.Property(ec => ec.CreatedDateTime)
        .IsRequired();

      sb.Property(ec => ec.ExpiredDateTime)
        .IsRequired();
    });
  }

  private void ConfigureRefreshTokensTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.RefreshTokens, sb =>
    {
      sb.ToTable("RefreshTokens");

      sb.WithOwner().HasForeignKey("UserId");

      sb.HasKey("Id", "UserId");

      sb.Property(rt => rt.Id)
        .HasColumnName("RefreshTokenId")
        .ValueGeneratedNever()
        .HasConversion(
          id => id.Value,
          value => RefreshTokenId.Create(value));

      sb.Property(rt => rt.ExpiredDateTime)
        .IsRequired();
    });

    builder.Metadata.FindNavigation(nameof(User.RefreshTokens))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureUserClaimsTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.Claims, sb =>
    {
      sb.ToTable("UserClaims");

      sb.WithOwner().HasForeignKey("UserId");

      sb.HasKey("Id", "UserId");

      sb.Property(c => c.Id)
        .HasColumnName("UserClaimId")
        .ValueGeneratedNever()
        .HasConversion(
          id => id.Value,
          value => UserClaimId.Create(value));

      sb.Property(c => c.Type)
        .IsRequired();

      sb.Property(c => c.Value)
        .IsRequired();
    });

    builder.Metadata.FindNavigation(nameof(User.Claims))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureUserRolesTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.Roles, sb =>
    {
      sb.ToTable("UserRoles");

      sb.WithOwner().HasForeignKey("UserId");

      sb.HasKey("Id", "UserId");

      sb.Property(r => r.Id)
        .HasColumnName("UserRoleId")
        .ValueGeneratedNever()
        .HasConversion(
          id => id.Value,
          value => UserRoleId.Create(value));

      sb.Property(r => r.Name)
        .IsRequired();

      sb.Property(r => r.NormalizedName)
        .IsRequired();
    });

    builder.Metadata.FindNavigation(nameof(User.Roles))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
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
      .HasConversion(
        id => id.Value,
        value => UserId.Create(value));

    builder.Property(u => u.FirstName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.LastName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.NickName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.Email)
      .IsRequired();

    builder.Property(u => u.IsEmailConfirmed)
      .IsRequired()
      .HasDefaultValue(false);
  }
}
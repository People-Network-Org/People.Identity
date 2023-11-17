using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.ApiKeyAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistance.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
  public void Configure(EntityTypeBuilder<ApiKey> builder)
  {
    ConfigureApiKeyTable(builder);
  }

  private void ConfigureApiKeyTable(EntityTypeBuilder<ApiKey> builder)
  {
    builder.ToTable("ApiKeys");

    builder.HasKey(k => k.Id);

    builder.HasIndex(k => k.Key).IsUnique();

    builder.Property(k => k.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ApiKeyId.Create(value)
      );

    builder.Property(k => k.Key)
      .IsRequired();
  }
}
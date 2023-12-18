using Microsoft.EntityFrameworkCore;

using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate;
using People.Identity.Infrastructure.Persistance.Interceptors;

namespace People.Identity.Infrastructure.Persistance;

public class IdentityDbContext : DbContext
{
  private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

  public IdentityDbContext(DbContextOptions<IdentityDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)
    : base(options)
  {
    _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
  }

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<ApiKey> ApiKeys { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Ignore<List<IDomainEvent>>()
      .ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

    modelBuilder.HasDefaultSchema("identity");
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
    base.OnConfiguring(optionsBuilder);
  }
}
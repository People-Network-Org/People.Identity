using Microsoft.EntityFrameworkCore;

using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate;
using People.Identity.Infrastructure.Persistence.Interceptors;

namespace People.Identity.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
  private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

  public IdentityDbContext(DbContextOptions<IdentityDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)
    : base(options)
  {
    _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
  }

  public DbSet<User> Users { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .Ignore<List<IDomainEvent>>()
      .ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

    base.OnModelCreating(modelBuilder);
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
    base.OnConfiguring(optionsBuilder);
  }
}
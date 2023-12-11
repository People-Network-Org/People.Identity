using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using People.Identity.Infrastructure.Persistance;

namespace People.Identity.Infrastructure.Common.Data;

public static class PrepareDB
{
  public static void Prepare(IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    var logger = scope.ServiceProvider.GetService<ILoggerFactory>()!.CreateLogger(nameof(PrepareDB));
    var dbContext = scope.ServiceProvider.GetService<IdentityDbContext>()!;
    Migrate(dbContext, logger);
  }

  private static void Migrate(IdentityDbContext context, ILogger logger)
  {
    try
    {
      logger.LogInformation("Starting database migration");
      context.Database.Migrate();
    }
    catch (Exception ex)
    {
      logger.LogCritical($"Could not migrate to database {ex.Message}");
      throw;
    }
  }
}
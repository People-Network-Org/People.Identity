using Microsoft.AspNetCore.Mvc.Infrastructure;
using People.Identity.Api.Common.Errors;
using People.Identity.Api.Common.Mapping;

namespace People.Identity.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {

    services.AddControllers();

    services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

    services.AddMappings();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    return services;
  }
}
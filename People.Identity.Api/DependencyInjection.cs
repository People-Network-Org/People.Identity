using System.Reflection;

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
    services.AddSwaggerGen(options =>
    {
      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    return services;
  }
}
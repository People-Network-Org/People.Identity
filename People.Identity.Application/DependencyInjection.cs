using System.Reflection;

using FluentValidation;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using People.Identity.Application.Common.Behaviors;

namespace People.Identity.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    var config = TypeAdapterConfig.GlobalSettings;
    config.Scan(Assembly.GetExecutingAssembly());

    return services;
  }
}
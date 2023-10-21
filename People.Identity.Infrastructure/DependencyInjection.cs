using System.Net.Security;
using System.Text;

using MassTransit;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.MassTransit;
using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Infrastructure.Authentication;
using People.Identity.Infrastructure.MassTransit;
using People.Identity.Infrastructure.Persistence;
using People.Identity.Infrastructure.Persistence.Interceptors;
using People.Identity.Infrastructure.Persistence.Repositories;

namespace People.Identity.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    services.AddAuth(configuration)
      .AddPersistance(configuration);

    return services;
  }

  public static IServiceCollection AddPersistance(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    services.AddDbContext<IdentityDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("PGSQL")));

    var massTransitSettings = new MassTransitSettings();
    configuration.Bind(MassTransitSettings.SectionName, massTransitSettings);

    services.AddMassTransit(x =>
    {
      x.UsingRabbitMq((context, cfg) =>
      {
        cfg.Host(massTransitSettings.Host, "/", h =>
        {
          h.Username(massTransitSettings.Username);
          h.Password(massTransitSettings.Password);
        });

        cfg.ConfigureEndpoints(context);
      });
    });

    services.AddScoped<IEventPublisher, EventPublisher>();
    services.AddScoped<PublishDomainEventsInterceptor>();
    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    var jwtSettings = new JwtSettings();
    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(jwtSettings));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    services.AddAuthorization();
    services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
      });

    return services;
  }
}
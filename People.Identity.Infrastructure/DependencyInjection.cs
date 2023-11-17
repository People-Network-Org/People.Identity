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
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Infrastructure.Auth;
using People.Identity.Infrastructure.MassTransit;
using People.Identity.Infrastructure.Persistance;
using People.Identity.Infrastructure.Persistance.Consumers;
using People.Identity.Infrastructure.Persistance.Interceptors;
using People.Identity.Infrastructure.Persistance.Repositories;
using People.Shared.Auth.ApiKey;

namespace People.Identity.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    services.AddPersistance(configuration)
      .AddAuth(configuration);

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
      x.SetEndpointNameFormatter(new DefaultEndpointNameFormatter(true));

      x.AddConsumer<UserConsumer>().Endpoint(cfg =>
      {
        cfg.ConcurrentMessageLimit = 10;
      });

      x.UsingRabbitMq((context, cfg) =>
      {
        cfg.Host(massTransitSettings.Host, "/", h =>
        {
          h.Username(massTransitSettings.Username);
          h.Password(massTransitSettings.Password);
        });

        cfg.UseDelayedRedelivery(r => r.Intervals(
          new[] { 5, 15, 30 }.Select(t => TimeSpan.FromMinutes(t)).ToArray()
        ));
        cfg.UseMessageRetry(r =>
        {
          r.Intervals(
            new[] { 5, 20, 60 }.Select(t => TimeSpan.FromSeconds(t)).ToArray()
          );
          r.Ignore<ArgumentNullException>();
        });

        cfg.ConfigureEndpoints(context);
      });
    });

    services.AddScoped<IEventPublisher, EventPublisher>();
    services.AddScoped<PublishDomainEventsInterceptor>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IApiKeyRepository, ApiKeyRepository>();

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
    services.AddHttpContextAccessor();
    services.AddApiKeyAuth<ApiKeyValidator>();

    services.AddAuthorization(options =>
    {
      options.AddApiKeyPolicy(policy =>
      {
        policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
      });
    });
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
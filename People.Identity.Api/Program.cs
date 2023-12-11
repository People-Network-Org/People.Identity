using People.Identity.Api;
using People.Identity.Application;
using People.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddPresentation()
  .AddApplication()
  .AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.PrepareInfrastructure();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz");
app.MapControllers();

app.Run();

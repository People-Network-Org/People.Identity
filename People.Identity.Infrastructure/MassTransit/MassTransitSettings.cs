namespace People.Identity.Infrastructure.MassTransit;

public class MassTransitSettings
{
  public const string SectionName = "MassTransit";

  public string Host { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string Password { get; set; } = null!;
}
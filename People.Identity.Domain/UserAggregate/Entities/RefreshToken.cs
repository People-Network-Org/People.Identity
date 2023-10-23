using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Domain.UserAggregate.Entities;

public class RefreshToken : Entity<RefreshTokenId>
{
  public string? Ip { get; private set; }
  public string? Device { get; private set; }
  public DateTime ExpiredDateTime { get; private set; }

  private RefreshToken(
    RefreshTokenId token,
    DateTime expiredDateTime,
    string? ip,
    string? device) : base(token)
  {
    ExpiredDateTime = expiredDateTime;
    Ip = ip;
    Device = device;
  }

  public static RefreshToken Create(
    string? ip,
    string? device)
  {
    var expiredDateTime = DateTime.UtcNow.AddMonths(6);
    return new(RefreshTokenId.CreateUnique(), expiredDateTime, ip, device);
  }

#pragma warning disable CS8618
  private RefreshToken()
  {
  }
#pragma warning restore CS8618
}
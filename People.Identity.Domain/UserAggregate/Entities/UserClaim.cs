using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Domain.UserAggregate.Entities;

public class UserClaim : Entity<UserClaimId>
{
  public string Type { get; private set; }
  public string Value { get; private set; }

  private UserClaim(UserClaimId roleId, string type, string value) : base(roleId)
  {
    Type = type;
    Value = value;
  }

  public static UserClaim Create(string type, string value)
  {
    return new(UserClaimId.CreateUnique(), type, value);
  }

#pragma warning disable CS8618
  private UserClaim()
  {
  }
#pragma warning restore CS8618
}
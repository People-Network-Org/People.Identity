using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.ValueObjects;

public class UserClaimId : ValueObject
{
  public Guid Value { get; }

  private UserClaimId(Guid value)
  {
    Value = value;
  }

  public static UserClaimId CreateUnique()
  {
    return new(Guid.NewGuid());
  }

  public static UserClaimId Create(Guid value)
  {
    return new UserClaimId(value);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
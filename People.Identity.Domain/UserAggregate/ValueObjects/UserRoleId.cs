using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.ValueObjects;

public class UserRoleId : ValueObject
{
  public Guid Value { get; }

  private UserRoleId(Guid value)
  {
    Value = value;
  }

  public static UserRoleId CreateUnique()
  {
    return new(Guid.NewGuid());
  }

  public static UserRoleId Create(Guid value)
  {
    return new UserRoleId(value);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
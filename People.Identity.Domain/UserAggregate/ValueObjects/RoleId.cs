using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.ValueObjects;

public class RoleId : ValueObject
{
  public Guid Value { get; }

  private RoleId(Guid value)
  {
    Value = value;
  }

  public static RoleId CreateUnique()
  {
    return new(Guid.NewGuid());
  }

  public static RoleId Create(Guid value)
  {
    return new RoleId(value);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
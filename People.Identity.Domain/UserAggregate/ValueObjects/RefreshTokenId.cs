using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.ValueObjects;

public class RefreshTokenId : ValueObject
{
  public string Value { get; }

  private RefreshTokenId(string value)
  {
    Value = value;
  }

  public static RefreshTokenId CreateUnique()
  {
    return new(Guid.NewGuid().ToString().Replace("-", string.Empty));
  }

  public static RefreshTokenId Create(string value)
  {
    return new RefreshTokenId(value);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
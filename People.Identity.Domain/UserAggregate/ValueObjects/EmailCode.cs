using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.ValueObjects;

public sealed class EmailCode : ValueObject
{
  public string Code { get; private set; }
  public DateTime CreatedDateTime { get; private set; }
  public DateTime ExpiredDateTime { get; private set; }

  private EmailCode(
    string code,
    DateTime createdDateTime,
    DateTime expiredDateTime)
  {
    Code = code;
    CreatedDateTime = createdDateTime;
    ExpiredDateTime = expiredDateTime;
  }

  public static EmailCode Create(
    string code,
    DateTime createdDateTime,
    DateTime expiredDateTime)
  {
    return new(code, createdDateTime, expiredDateTime);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Code;
    yield return CreatedDateTime;
    yield return ExpiredDateTime;
  }
}
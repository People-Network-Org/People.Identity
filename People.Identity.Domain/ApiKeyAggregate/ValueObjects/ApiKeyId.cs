using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.ApiKeyAggregate.ValueObjects;

public sealed class ApiKeyId : AggregateRootId<Guid>
{
  public override Guid Value { get; protected set; }

  private ApiKeyId(Guid value)
  {
    Value = value;
  }

  public static ApiKeyId CreateUnique()
  {
    return new(Guid.NewGuid());
  }

  public static ApiKeyId Create(Guid value)
  {
    return new(value);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
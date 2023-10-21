using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Domain.UserAggregate.Entities;

public class Role : Entity<RoleId>
{
  public string Name { get; }
  public string NormalizedName { get; }

  private Role(RoleId roleId, string name) : base(roleId)
  {
    Name = name;
    NormalizedName = name.ToUpper();
  }

  public static Role Create(string name)
  {
    return new(RoleId.CreateUnique(), name);
  }
}
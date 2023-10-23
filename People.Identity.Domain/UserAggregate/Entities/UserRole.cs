using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Domain.UserAggregate.Entities;

public class UserRole : Entity<UserRoleId>
{
  public string Name { get; private set; }
  public string NormalizedName { get; private set; }

  private UserRole(UserRoleId roleId, string name) : base(roleId)
  {
    Name = name;
    NormalizedName = name.ToUpper();
  }

  public static UserRole Create(string name)
  {
    return new(UserRoleId.CreateUnique(), name);
  }

#pragma warning disable CS8618
  private UserRole()
  {
  }
#pragma warning restore CS8618
}
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.UserMediatR.Common;

public static class UserUtils
{
  public static UserRole? GetUserRole(User user, string role)
  {
    return user.Roles.FirstOrDefault(r => r.NormalizedName == role.ToUpper());
  }

  public static UserClaim? GetUserClaim(User user, string type, string value)
  {
    return user.Claims.FirstOrDefault(c =>
      c.Type == type &&
      c.Value == value);
  }

  public static UserClaim? GetUserClaim(User user, string type)
  {
    return user.Claims.FirstOrDefault(c =>
      c.Type == type);
  }

  public static User? GetUserById(Guid id, IUserRepository userRepository)
  {
    return userRepository.GetById(UserId.Create(id));
  }
}
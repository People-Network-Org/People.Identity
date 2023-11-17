using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
  User? GetById(UserId id);
  User? GetByEmailCode(string emailCode);
  User? GetUserByEmail(string email);
  User? GetUserByRefreshToken(string refreshToken);
  void Add(User user);
  void Update(User user);
}
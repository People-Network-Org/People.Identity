using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
  User? GetById(string id);
  User? GetUserByEmail(string email);
  User? GetUserByRefreshToken(string refreshToken);
  void Add(User user);
  void Update(User user);
}
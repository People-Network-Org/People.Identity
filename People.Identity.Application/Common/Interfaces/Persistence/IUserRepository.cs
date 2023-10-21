using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
  User? GetUserByEmail(string email);
  void Add(User user);
}
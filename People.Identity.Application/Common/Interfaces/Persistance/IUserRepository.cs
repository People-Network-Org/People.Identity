using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.Common.Interfaces.Persistance;

public interface IUserRepository : IRepository<User>
{
  User? GetById(UserId id);
  User? GetByEmailCode(string emailCode);
  User? GetUserByEmail(string email);
  User? GetUserByNickName(string nickName);
  User? GetUserByRefreshToken(string refreshToken);
  ICollection<User> GetAllByIds(ICollection<UserId> userIds);
}
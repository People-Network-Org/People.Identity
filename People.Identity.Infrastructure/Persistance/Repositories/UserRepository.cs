using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistance.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
  public UserRepository(IdentityDbContext dbContext) : base(dbContext)
  {
  }

  public User? GetUserByEmail(string email)
  {
    return _dbContext.Users.FirstOrDefault(u => u.Email == email);
  }

  public User? GetUserByRefreshToken(string refreshToken)
  {
    return _dbContext.Users.Where(u => u.RefreshTokens.Any(rt => rt.Id == RefreshTokenId.Create(refreshToken))).FirstOrDefault();
  }

  public User? GetById(UserId id)
  {
    return _dbContext.Users.FirstOrDefault(u => u.Id == id);
  }

  public User? GetByEmailCode(string emailCode)
  {
    return _dbContext.Users.FirstOrDefault(u => u.EmailCode! != null! && u.EmailCode.Code == emailCode);
  }

  public ICollection<User> GetAllByIds(ICollection<UserId> userIds)
  {
    return _dbContext.Users.Where(u => userIds.Contains(u.Id)).ToList();
  }

}
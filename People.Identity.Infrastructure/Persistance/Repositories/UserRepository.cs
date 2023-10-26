using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
  private readonly IdentityDbContext _dbContext;

  public UserRepository(IdentityDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void Add(User user)
  {
    _dbContext.Add(user);
    _dbContext.SaveChanges();
  }

  public void Update(User user)
  {
    _dbContext.Update(user);
    _dbContext.SaveChanges();
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
}
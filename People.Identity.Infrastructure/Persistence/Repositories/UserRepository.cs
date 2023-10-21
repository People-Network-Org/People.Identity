using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.UserAggregate;

namespace People.Identity.Infrastructure.Persistence.Repositories;

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

  public User? GetUserByEmail(string email)
  {
    return _dbContext.Users.FirstOrDefault(u => u.Email == email);
  }
}
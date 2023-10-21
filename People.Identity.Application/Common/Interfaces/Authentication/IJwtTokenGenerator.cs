using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);
}
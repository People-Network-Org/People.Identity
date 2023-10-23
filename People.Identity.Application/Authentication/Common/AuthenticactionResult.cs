using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token, string RefreshToken);
using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Identity;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetUserByEmail(query.Email) is not User user)
      return Errors.Authentication.InvalidCredentials;

    var verifiedHash = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, query.Password);
    // var passwordHash = new PasswordHasher<User>().HashPassword(user, query.Password);

    // if (user.PasswordHash != passwordHash)
    if (verifiedHash == PasswordVerificationResult.Failed)
      return Errors.Authentication.InvalidCredentials;

    var token = _jwtTokenGenerator.GenerateToken(user);
    var refreshToken = RefreshToken.Create(null, null);
    user.AddRefreshToken(refreshToken);
    _userRepository.Update(user);

    return new AuthenticationResult(user, token, refreshToken.Id.Value);
  }
}
using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;

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

    if (user.Password != query.Password)
      return Errors.Authentication.InvalidCredentials;

    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }
}
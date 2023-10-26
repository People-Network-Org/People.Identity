using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.Authentication.Commands.Refresh;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, ErrorOr<RefreshResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public RefreshCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<ErrorOr<RefreshResult>> Handle(RefreshCommand command, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetUserByRefreshToken(command.RefreshToken) is not User user)
      return Errors.User.NotValidRefreshToken;

    var oldRefreshToken = user.RefreshTokens.First(rt => rt.Id.Value == command.RefreshToken);
    user.RemoveRefreshToken(oldRefreshToken);

    var newRefreshToken = RefreshToken.Create(null, null);
    user.AddRefreshToken(newRefreshToken);
    _userRepository.Update(user);

    var token = _jwtTokenGenerator.GenerateToken(user);
    return new RefreshResult(token, newRefreshToken.Id.Value);
  }
}
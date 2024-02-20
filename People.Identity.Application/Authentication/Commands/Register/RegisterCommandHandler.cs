using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetUserByNickName(command.NickName) is not null)
      return Errors.User.DuplicateNickName;

    if (command.Email != null && _userRepository.GetUserByEmail(command.Email) is not null)
      return Errors.User.DuplicateEmail;

    var user = User.Create(
      command.FirstName,
      command.LastName,
      command.NickName,
      command.Email,
      false,
      null,
      null);

    _userRepository.Add(user);

    return new RegisterResult(user);
  }
}
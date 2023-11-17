using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.UserMediatR.Queries.ConfirmationUser;

public class ConfirmationUserQueryHandler : IRequestHandler<ConfirmationUserQuery, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;

  public ConfirmationUserQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<UserResult>> Handle(ConfirmationUserQuery request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetByEmailCode(request.EmailCode) is not User user
      || user.EmailCode!.ExpiredDateTime < DateTime.UtcNow)
    {
      return Errors.User.UserNotFound;
    }

    return new UserResult(user);
  }
}

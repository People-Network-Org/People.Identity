using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.UserMediatR.Queries.UserById;

public class UserQueryHandler : IRequestHandler<UserQuery, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;

  public UserQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<UserResult>> Handle(UserQuery request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(request.Id) is not User user)
      return Errors.User.UserNotFound;

    return new UserResult(user);
  }
}
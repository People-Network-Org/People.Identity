using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistance;

using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;

namespace People.Identity.Application.UserMediatR.Queries.Collection;

public class CollectionHandler : IRequestHandler<CollectionQuery, ErrorOr<List<UserResult>>>
{
  private readonly IUserRepository _userRepository;

  public CollectionHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }


  public async Task<ErrorOr<List<UserResult>>> Handle(CollectionQuery request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    var users = _userRepository.GetAllByIds(request.UserIds);

    if (users.Count != request.UserIds.Count)
      return Errors.User.UserNotFound;

    return users.Select(u => new UserResult(u)).ToList();
  }
}

using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;

namespace People.Identity.Application.UserMediatR.Commands.AddRole;

public record AddRoleCommand(
  Guid UserId,
  string Role) : IRequest<ErrorOr<UserResult>>;
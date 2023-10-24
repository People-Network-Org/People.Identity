using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;

namespace People.Identity.Application.UserMediatR.Commands.RemoveRole;

public record RemoveRoleCommand(
  Guid UserId,
  string Role) : IRequest<ErrorOr<UserResult>>;
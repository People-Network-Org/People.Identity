using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;

namespace People.Identity.Application.UserMediatR.Commands.RemoveClaim;

public record RemoveClaimCommand(
  Guid UserId,
  string Type,
  string Value) : IRequest<ErrorOr<UserResult>>;
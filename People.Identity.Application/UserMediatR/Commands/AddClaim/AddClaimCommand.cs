using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;

namespace People.Identity.Application.UserMediatR.Commands.AddClaim;

public record AddClaimCommand(
  Guid UserId,
  string Type,
  string Value) : IRequest<ErrorOr<UserResult>>;
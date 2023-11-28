using ErrorOr;

using MediatR;

using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.Authentication.Commands.Revoke;

public record RevokeCommand(UserId UserId, RefreshTokenId RefreshToken) : IRequest<ErrorOr<bool>>;
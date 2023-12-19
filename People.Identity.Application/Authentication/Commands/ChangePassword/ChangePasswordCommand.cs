using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand(UserId UserId, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
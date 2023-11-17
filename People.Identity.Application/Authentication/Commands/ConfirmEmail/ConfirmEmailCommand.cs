using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;

namespace People.Identity.Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string EmailCode, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
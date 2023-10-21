using ErrorOr;
using MediatR;
using People.Identity.Application.Authentication.Common;

namespace People.Identity.Application.Authentication.Commands.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password) : IRequest<ErrorOr<AuthenticationResult>>;
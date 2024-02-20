using ErrorOr;

using MediatR;

namespace People.Identity.Application.Authentication.Commands.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string NickName,
  string? Email) : IRequest<ErrorOr<RegisterResult>>;
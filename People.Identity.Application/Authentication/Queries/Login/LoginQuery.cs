using ErrorOr;
using MediatR;
using People.Identity.Application.Authentication.Common;

namespace People.Identity.Application.Authentication.Queries.Login;

public record LoginQuery(
  string Email,
  string Password) : IRequest<ErrorOr<AuthenticationResult>>;
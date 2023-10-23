using ErrorOr;

using MediatR;

namespace People.Identity.Application.Authentication.Commands.Refresh;

public record RefreshCommand(string RefreshToken) : IRequest<ErrorOr<RefreshResult>>;
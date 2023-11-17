using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;

namespace People.Identity.Application.Authentication.Commands.DeleteKey;

public record DeleteKeyCommand(string Key) : IRequest<ErrorOr<ApiKeyResult>>;

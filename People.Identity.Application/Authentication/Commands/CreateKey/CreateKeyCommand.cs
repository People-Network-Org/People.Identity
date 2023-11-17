using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;

namespace People.Identity.Application.Authentication.Commands.CreateKey;

public record CreateKeyCommand() : IRequest<ErrorOr<ApiKeyResult>>;

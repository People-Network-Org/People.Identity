using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.UserMediatR.Commands.Delete;

public record DeleteCommand(UserId UserId) : IRequest<ErrorOr<UserResult>>;
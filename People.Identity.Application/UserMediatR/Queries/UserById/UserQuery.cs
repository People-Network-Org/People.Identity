using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.UserMediatR.Queries.UserById;

public record UserQuery(UserId Id) : IRequest<ErrorOr<UserResult>>;
using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;

using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Application.UserMediatR.Queries.Collection;

public record CollectionQuery(ICollection<UserId> UserIds) : IRequest<ErrorOr<List<UserResult>>>;
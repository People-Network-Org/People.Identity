using ErrorOr;

using MediatR;

using People.Identity.Application.UserMediatR.Common;

namespace People.Identity.Application.UserMediatR.Queries.ConfirmationUser;

public record ConfirmationUserQuery(string EmailCode) : IRequest<ErrorOr<UserResult>>;
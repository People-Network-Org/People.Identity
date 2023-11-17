using People.Identity.Domain.UserAggregate;

namespace People.Identity.Application.Authentication.Commands.Register;

public record RegisterResult(User User);
using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.Events;

public record UserConfirmed(User User) : IDomainEvent;

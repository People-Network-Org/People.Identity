using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.UserAggregate.Events;

public record UserCreated(User User) : IDomainEvent;

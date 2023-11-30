using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.ApiKeyAggregate.Events;

public record ApiKeyCreated(ApiKey ApiKey) : IDomainEvent;
namespace People.Identity.Contracts.Common;

public record CollectionResponse<T>(ICollection<T> Items);
namespace People.Identity.Contracts.User;

public record UserResponse(
  Guid Id,
  string FirstName,
  string LastName,
  string NickName,
  string Email,
  string? Phone
);
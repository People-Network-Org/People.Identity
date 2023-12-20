namespace People.Identity.Contracts.User;

public record UserAdminResponse(
  Guid Id,
  string FirstName,
  string LastName,
  string NickName,
  string Email,
  string? Phone,
  bool IsConfirmed,
  string? EmailCode
);
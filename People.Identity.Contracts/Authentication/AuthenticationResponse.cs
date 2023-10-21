namespace People.Identity.Contracts.Authentication;

public record AuthenticationResponse(Guid Id, string FirstName, string LastName, string NickName, string Email, string Phone, string Token);
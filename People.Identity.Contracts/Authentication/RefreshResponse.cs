namespace People.Identity.Contracts.Authentication;

public record RefreshResponse(string Token, string RefreshToken);
namespace People.Identity.Contracts.Authentication;

public record ConfirmEmailRequest(string EmailCode, string Password);
using ErrorOr;

namespace People.Identity.Domain.Common.Errors;

public static partial class Errors
{
  public static class Authentication
  {
    public static Error InvalidCredentials => Error.Validation(code: "User.InvalidCredentials", description: "Данные для входа введены неверно");
  }
}
using ErrorOr;

namespace People.Identity.Domain.Common.Errors;

public static partial class Errors
{
  public static class User
  {
    public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicateEmail", description: "Адрес электронной почты уже зарегистрирован");
  }
}
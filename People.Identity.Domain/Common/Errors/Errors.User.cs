using ErrorOr;

namespace People.Identity.Domain.Common.Errors;

public static partial class Errors
{
  public static class User
  {
    public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicateEmail", description: "Адрес электронной почты уже зарегистрирован");
    public static Error NotValidRefreshToken => Error.Validation(code: "User.NotValidRefreshToken", description: "Недействительный Refresh Token");
    public static Error UserNotFound => Error.NotFound(code: "User.NotFound", description: "Такой пользователь не существует");
    public static Error UserAlreadyHasClaim => Error.Conflict(code: "User.AlreadyHasClaim", description: "У пользователя уже есть этот Claim");
    public static Error UserAlreadyHasRole => Error.Conflict(code: "User.AlreadyHasRole", description: "У пользователя уже есть эта роль");
  }
}
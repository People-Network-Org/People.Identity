using FluentValidation;

namespace People.Identity.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
  public ChangePasswordValidator()
  {
    RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
  }
}
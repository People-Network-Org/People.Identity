using FluentValidation;

namespace People.Identity.Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
  public ConfirmEmailCommandValidator()
  {
    RuleFor(x => x.EmailCode).NotEmpty();
    RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
  }
}
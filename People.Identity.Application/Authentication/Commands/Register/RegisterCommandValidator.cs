using FluentValidation;

namespace People.Identity.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
  public RegisterCommandValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.NickName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
  }
}
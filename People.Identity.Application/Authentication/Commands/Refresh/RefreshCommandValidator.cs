using FluentValidation;

namespace People.Identity.Application.Authentication.Commands.Refresh;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
  public RefreshCommandValidator()
  {
    RuleFor(x => x.RefreshToken).NotEmpty();
  }
}
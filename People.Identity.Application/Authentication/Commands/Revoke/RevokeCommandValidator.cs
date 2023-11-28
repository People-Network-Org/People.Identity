using FluentValidation;

namespace People.Identity.Application.Authentication.Commands.Revoke;

public class RevokeCommandValidator : AbstractValidator<RevokeCommand>
{
  public RevokeCommandValidator()
  {
    RuleFor(x => x.RefreshToken).NotEmpty();
  }
}
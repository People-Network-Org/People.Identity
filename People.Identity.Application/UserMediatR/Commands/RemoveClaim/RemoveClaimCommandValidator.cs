using FluentValidation;

namespace People.Identity.Application.UserMediatR.Commands.RemoveClaim;

public class RemoveClaimCommandValidator : AbstractValidator<RemoveClaimCommand>
{
  public RemoveClaimCommandValidator()
  {
    RuleFor(x => x.UserId).NotEmpty();
    RuleFor(x => x.Value).NotEmpty();
    RuleFor(x => x.Type).NotEmpty();
  }
}
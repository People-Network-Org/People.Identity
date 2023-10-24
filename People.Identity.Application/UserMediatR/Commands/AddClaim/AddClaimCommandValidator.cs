using FluentValidation;

namespace People.Identity.Application.UserMediatR.Commands.AddClaim;

public class AddClaimCommandValidator : AbstractValidator<AddClaimCommand>
{
  public AddClaimCommandValidator()
  {
    RuleFor(x => x.UserId).NotEmpty();
    RuleFor(x => x.Value).NotEmpty();
    RuleFor(x => x.Type).NotEmpty();
  }
}
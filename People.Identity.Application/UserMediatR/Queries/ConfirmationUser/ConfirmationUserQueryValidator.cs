using FluentValidation;

namespace People.Identity.Application.UserMediatR.Queries.ConfirmationUser;

public class ConfirmationUserQueryValidator : AbstractValidator<ConfirmationUserQuery>
{
  public ConfirmationUserQueryValidator()
  {
    RuleFor(x => x.EmailCode).NotEmpty();
  }
}
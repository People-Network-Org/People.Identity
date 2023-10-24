using FluentValidation;

namespace People.Identity.Application.UserMediatR.Commands.RemoveRole;

public class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
{
  public RemoveRoleCommandValidator()
  {
    RuleFor(x => x.UserId).NotEmpty();
    RuleFor(x => x.Role).NotEmpty();
  }
}
using FluentValidation;

namespace People.Identity.Application.UserMediatR.Commands.AddRole;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
  public AddRoleCommandValidator()
  {
    RuleFor(x => x.UserId).NotEmpty();
    RuleFor(x => x.Role).NotEmpty();
  }
}
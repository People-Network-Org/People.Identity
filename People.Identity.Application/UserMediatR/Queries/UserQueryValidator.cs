using FluentValidation;

namespace People.Identity.Application.UserMediatR.Queries;

public class UserQueryValidator : AbstractValidator<UserQuery>
{
  public UserQueryValidator()
  {
  }
}
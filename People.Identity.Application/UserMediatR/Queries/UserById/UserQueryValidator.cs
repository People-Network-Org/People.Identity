using FluentValidation;

namespace People.Identity.Application.UserMediatR.Queries.UserById;

public class UserQueryValidator : AbstractValidator<UserQuery>
{
  public UserQueryValidator()
  {
  }
}
using Microsoft.AspNetCore.Identity;

using People.Identity.Domain.Common.Models;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.Events;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
  private readonly List<UserClaim> _claims = new();
  private readonly List<UserRole> _roles = new();
  private readonly List<RefreshToken> _refreshTokens = new();

  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public string NickName { get; private set; }
  public string Email { get; private set; }
  public string? Phone { get; private set; }
  public string PasswordHash { get; private set; }

  public IReadOnlyList<UserClaim> Claims => _claims.ToList().AsReadOnly();
  public IReadOnlyList<UserRole> Roles => _roles.ToList().AsReadOnly();
  public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.ToList().AsReadOnly();

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  private User(
      UserId userId,
      string firstName,
      string lastName,
      string nickName,
      string email,
      string? phone,
      string passwordHash,
      DateTime createdDateTime,
      DateTime updatedDateTime) : base(userId)
  {
    FirstName = firstName;
    LastName = lastName;
    NickName = nickName;
    Email = email;
    Phone = phone;
    PasswordHash = passwordHash;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = updatedDateTime;
  }

  public static User Create(
      string firstName,
      string lastName,
      string nickName,
      string email,
      string? phone,
      string password)
  {
    var passwordHash = new PasswordHasher<User>().HashPassword(null!, password);
    var user = new User(
        UserId.CreateUnique(),
        firstName,
        lastName,
        nickName,
        email,
        phone,
        passwordHash,
        DateTime.UtcNow,
        DateTime.UtcNow);

    user.AddDomainEvent(new UserCreated(user));

    return user;
  }

  public void AddClaim(UserClaim claim)
  {
    _claims.Add(claim);
  }

  public void AddRole(UserRole role)
  {
    _roles.Add(role);
  }

  public void AddRefreshToken(RefreshToken refreshToken)
  {
    _refreshTokens.Add(refreshToken);
  }

  public void RemoveRefreshToken(RefreshToken refreshToken)
  {
    _refreshTokens.Remove(refreshToken);
  }

#pragma warning disable CS8618
  private User()
  {
  }
#pragma warning restore CS8618
}
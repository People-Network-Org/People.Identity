using System.IdentityModel.Tokens.Jwt;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using People.Identity.Application.Authentication.Commands.ChangePassword;
using People.Identity.Application.Authentication.Commands.ConfirmEmail;
using People.Identity.Application.Authentication.Commands.Refresh;
using People.Identity.Application.Authentication.Commands.Register;
using People.Identity.Application.Authentication.Commands.Revoke;
using People.Identity.Application.Authentication.Queries.Login;
using People.Identity.Application.UserMediatR.Queries.ConfirmationUser;
using People.Identity.Application.UserMediatR.Queries.UserById;
using People.Identity.Contracts.Authentication;
using People.Identity.Contracts.User;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Api.Controllers;

public class AuthController : ApiController
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public AuthController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  /// <summary>
  /// Получить пользователя по коду подтверждения
  /// </summary>
  /// <param name="code">Код подтверждения</param>
  /// <returns>Информация о пользователе</returns>
  [HttpGet("confirm/{code}")]
  [AllowAnonymous]
  public async Task<IActionResult> GetConfirmationUser(string code)
  {
    var command = new ConfirmationUserQuery(code);
    var userResult = await _mediator.Send(command);

    return userResult.Match(
      result => Ok(_mapper.Map<UserResponse>(result)),
      Problem
    );
  }

  /// <summary>
  /// Подтвердить регистрацию пользователя по коду
  /// </summary>
  [HttpPost("confirm")]
  [AllowAnonymous]
  public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
  {
    var command = _mapper.Map<ConfirmEmailCommand>(request);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<AuthenticationResponse>(result)),
      Problem
    );
  }

  /// <summary>
  /// Зарегистрировать пользователя
  /// </summary>
  [HttpPost("register")]
  [AllowAnonymous]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = _mapper.Map<RegisterCommand>(request);
    var userResult = await _mediator.Send(command);

    return userResult.Match(
      result => base.Ok(_mapper.Map<UserResponse>(result)),
      Problem
    );
  }

  /// <summary>
  /// Авторизовать пользователя
  /// </summary>
  [HttpPost("login")]
  [AllowAnonymous]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var command = _mapper.Map<LoginQuery>(request);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<AuthenticationResponse>(result)),
      Problem
    );
  }

  /// <summary>
  /// Обновить JWT по Refresh Token
  /// </summary>
  [HttpPost("refresh")]
  [AllowAnonymous]
  public async Task<IActionResult> Refresh(RefreshRequest request)
  {
    var command = _mapper.Map<RefreshCommand>(request);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<RefreshResponse>(result)),
      Problem
    );
  }

  /// <summary>
  /// Выход пользователя из системы
  /// </summary>
  [HttpPost("logout")]
  public async Task<IActionResult> Logout(RefreshRequest request)
  {
    var idClaim = HttpContext.User.Claims.FirstOrDefault(c =>
      c.Type == JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub]);

    var notFoundProblem = new[] { Errors.User.UserNotFound }.ToList();
    if (idClaim is null)
      return Problem(notFoundProblem);

    if (!Guid.TryParse(idClaim.Value, out Guid guid))
      return Problem(notFoundProblem);

    var userId = UserId.Create(guid);
    var refreshTokenId = RefreshTokenId.Create(request.RefreshToken);
    var command = new RevokeCommand(userId, refreshTokenId);
    var result = await _mediator.Send(command);

    return result.Match(
      res => Ok(),
      Problem
    );
  }

  /// <summary>
  /// Изменить пароль пользователя
  /// </summary>
  [HttpPut("password")]
  public async Task<IActionResult> ChangePassword([FromBody] PasswordRequest request)
  {
    var idClaim = HttpContext.User.Claims.FirstOrDefault(c =>
      c.Type == JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub]);

    var notFoundProblem = new[] { Errors.User.UserNotFound }.ToList();
    if (idClaim is null)
      return Problem(notFoundProblem);

    if (!Guid.TryParse(idClaim.Value, out Guid guid))
      return Problem(notFoundProblem);

    var userId = UserId.Create(guid);
    var command = new ChangePasswordCommand(userId, request.Password);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<AuthenticationResponse>(result)),
      Problem
    );
  }

  /// <summary>
  /// Получить информацию об авторизованном пользователе
  /// </summary>
  [HttpGet("me")]
  public async Task<IActionResult> Me()
  {
    var idClaim = HttpContext.User.Claims.FirstOrDefault(c =>
      c.Type == JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub]);

    var notFoundProblem = new[] { Errors.User.UserNotFound }.ToList();
    if (idClaim is null)
      return Problem(notFoundProblem);

    if (!Guid.TryParse(idClaim.Value, out Guid guid))
      return Problem(notFoundProblem);

    var userId = UserId.Create(guid);
    var command = new UserQuery(userId);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<UserResponse>(result)),
      Problem
    );
  }
}
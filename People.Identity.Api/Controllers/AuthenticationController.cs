using System.IdentityModel.Tokens.Jwt;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using People.Identity.Application.Authentication.Commands.Refresh;
using People.Identity.Application.Authentication.Commands.Register;
using People.Identity.Application.Authentication.Queries.Login;
using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Application.UserMediatR.Queries;
using People.Identity.Contracts.Authentication;
using People.Identity.Contracts.User;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate.ValueObjects;

namespace People.Identity.Api.Controllers;

[Route("/auth")]
public class AuthenticationController : ApiController
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public AuthenticationController(ISender mediator, IMapper mapper, IUserRepository userRepository)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost("register")]
  [AllowAnonymous]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = _mapper.Map<RegisterCommand>(request);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<AuthenticationResponse>(result)),
      Problem
    );
  }

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
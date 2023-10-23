using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using People.Identity.Application.Authentication.Commands.Refresh;
using People.Identity.Application.Authentication.Commands.Register;
using People.Identity.Application.Authentication.Queries.Login;
using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Contracts.Authentication;

namespace People.Identity.Api.Controllers;

[Route("/auth")]
[AllowAnonymous]
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
  public async Task<IActionResult> Refresh(RefreshRequest request)
  {
    var command = _mapper.Map<RefreshCommand>(request);
    var authResult = await _mediator.Send(command);

    return authResult.Match(
      result => Ok(_mapper.Map<RefreshResponse>(result)),
      Problem
    );
  }
}
using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using People.Identity.Application.UserMediatR.Queries.Collection;
using People.Identity.Contracts.Common;
using People.Identity.Contracts.User;
using People.Shared.Constants;

namespace People.Identity.Api.Controllers;

public class UsersController : ApiController
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public UsersController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpGet("collection")]
  public async Task<IActionResult> Collection([FromQuery] ICollection<Guid> ids)
  {
    var modifyClaim = HttpContext.User.Claims.FirstOrDefault(c =>
      c.Type == JWTConstants.ModifyOrganizationClaimType);

    var query = _mapper.Map<CollectionQuery>(ids);
    var result = await _mediator.Send(query);

    return result.Match(
      result => Ok(modifyClaim is not null && bool.Parse(modifyClaim.Value) ?
        _mapper.Map<CollectionResponse<UserAdminResponse>>(result) :
        _mapper.Map<CollectionResponse<UserResponse>>(result)),
      Problem
    );
  }
}
using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using People.Identity.Application.UserMediatR.Commands.Delete;
using People.Identity.Application.UserMediatR.Queries.Collection;
using People.Identity.Contracts.Common;
using People.Identity.Contracts.User;
using People.Identity.Domain.UserAggregate.ValueObjects;
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

  /// <summary>
  /// Получить информацию о списке пользователей по их id
  /// </summary>
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

  /// <summary>
  /// Удалить пользователя
  /// </summary>
  [HttpDelete("{id:Guid}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    var modifyClaim = HttpContext.User.Claims.FirstOrDefault(c =>
      c.Type == JWTConstants.ModifyOrganizationClaimType);

    if (modifyClaim is null || !bool.Parse(modifyClaim.Value))
      return Forbid();

    var userId = UserId.Create(id);
    var command = new DeleteCommand(userId);
    var result = await _mediator.Send(command);

    return result.Match(
      result => Ok(_mapper.Map<UserResponse>(result)),
      Problem
    );
  }
}
using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using People.Identity.Application.Authentication.Commands.CreateKey;
using People.Identity.Application.Authentication.Commands.DeleteKey;
using People.Identity.Contracts.Authentication;

namespace People.Identity.Api.Controllers;

public class KeyController : ApiController
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public KeyController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost]
  public async Task<IActionResult> Create()
  {
    var command = new CreateKeyCommand();
    var keyResult = await _mediator.Send(command);

    return keyResult.Match(
      result => Ok(_mapper.Map<ApiKeyResponse>(result)),
      Problem
    );
  }

  [HttpDelete]
  public async Task<IActionResult> Delete(DeleteApiKeyRequest request)
  {
    var command = _mapper.Map<DeleteKeyCommand>(request);
    var keyResult = await _mediator.Send(command);

    return keyResult.Match(
      result => Ok(_mapper.Map<ApiKeyResponse>(result)),
      Problem
    );
  }
}
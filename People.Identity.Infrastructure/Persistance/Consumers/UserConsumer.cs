using MapsterMapper;

using MassTransit;

using MediatR;

using People.Identity.Application.UserMediatR.Commands.AddClaim;
using People.Identity.Application.UserMediatR.Commands.AddRole;
using People.Identity.Application.UserMediatR.Commands.RemoveClaim;
using People.Shared.AMQP.Events;
using People.Shared.AMQP.Tasks;
using People.Shared.Constants;

namespace People.Identity.Infrastructure.Persistance.Consumers;

public class UserConsumer :
  IConsumer<AddClaimToUser>,
  IConsumer<RemoveClaimFromUser>,
  IConsumer<AddRoleToUser>,
  IConsumer<RemoveRoleFromUser>,
  IConsumer<UserOrganizationEvent>
{
  private readonly IMapper _mapper;
  private readonly ISender _mediator;

  public UserConsumer(IMapper mapper, ISender mediator)
  {
    _mapper = mapper;
    _mediator = mediator;
  }

  public async Task Consume(ConsumeContext<AddClaimToUser> context)
  {
    var command = _mapper.Map<AddClaimCommand>(context.Message);
    var result = await _mediator.Send(command);
  }

  public async Task Consume(ConsumeContext<RemoveClaimFromUser> context)
  {
    var command = _mapper.Map<RemoveClaimCommand>(context.Message);
    var result = await _mediator.Send(command);
  }

  public async Task Consume(ConsumeContext<AddRoleToUser> context)
  {
    var command = _mapper.Map<AddRoleCommand>(context.Message);
    var result = await _mediator.Send(command);
  }

  public async Task Consume(ConsumeContext<RemoveRoleFromUser> context)
  {
    var command = _mapper.Map<RemoveRoleFromUser>(context.Message);
    var result = await _mediator.Send(command);
  }

  public async Task Consume(ConsumeContext<UserOrganizationEvent> context)
  {
    var orgCommand = new AddClaimCommand(context.Message.UserId, JWTConstants.OrganizationClaimType, context.Message.OrganizationId.ToString());
    var orgResult = await _mediator.Send(orgCommand);
    var modifyCommand = new AddClaimCommand(context.Message.UserId, JWTConstants.ModifyOrganizationClaimType, context.Message.CanModify.ToString());
    var modifyResult = await _mediator.Send(modifyCommand);
  }

}
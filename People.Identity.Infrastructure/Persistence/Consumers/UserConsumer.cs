using MapsterMapper;

using MassTransit;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Application.UserMediatR.Commands.AddClaim;
using People.Identity.Application.UserMediatR.Commands.AddRole;
using People.Identity.Application.UserMediatR.Commands.RemoveClaim;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Persistence.Consumers;

public class UserConsumer :
  IConsumer<AddClaimToUser>,
  IConsumer<RemoveClaimFromUser>,
  IConsumer<AddRoleToUser>,
  IConsumer<RemoveRoleFromUser>
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;
  private readonly ISender _mediator;

  public UserConsumer(IUserRepository userRepository, IMapper mapper, ISender mediator)
  {
    _userRepository = userRepository;
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
}
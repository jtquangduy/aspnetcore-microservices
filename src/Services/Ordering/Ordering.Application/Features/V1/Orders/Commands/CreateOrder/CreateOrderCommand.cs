using AutoMapper;
using EventBus.Messages.IntegrationEvent.Events;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Features.V1.Orders.Common;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder;

public class CreateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<long>>, IMapFrom<Order>, IMapFrom<BasketCheckoutEvent>
{
    public string UserName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrderCommand, Order>();
        profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
    }
}
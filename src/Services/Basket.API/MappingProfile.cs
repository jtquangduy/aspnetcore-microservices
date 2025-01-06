using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.IntegrationEvent.Events;

namespace Basket.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>();
    }
}
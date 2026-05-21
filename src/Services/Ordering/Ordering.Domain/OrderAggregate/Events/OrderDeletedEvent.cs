using Contracts.Common.Events;
namespace Ordering.Domain.OrderAggregate.Events;

public class OrderDeletedEvent : BaseEvent
{
    public OrderDeletedEvent(long id)
    {
        Id = id;
    }

    public long Id { get; }
}
using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events;

public class OrderCreatedEvent : BaseEvent
{
    public OrderCreatedEvent(long id, string userName, decimal totalPrice, string documentNo, string emailAddress,
        string shippingAddress, string invoiceAddress,
        string fullName)
    {
        Id = id;
        TotalPrice = totalPrice;
        UserName = userName;
        DocumentNo = documentNo;
        EmailAddress = emailAddress;
        ShippingAddress = shippingAddress;
        InvoiceAddress = invoiceAddress;
        FullName = fullName;
    }

    public long Id { get; }
    public string UserName { get; }
    public string DocumentNo { get; }
    public string EmailAddress { get; }
    public decimal TotalPrice { get; }
    public string ShippingAddress { get; }
    public string InvoiceAddress { get; }

    public string FullName { get; set; }
}
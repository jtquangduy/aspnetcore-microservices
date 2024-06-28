using Ordering.Appication.Common.Mappings;
using Ordering.Domain.Enities;

namespace Ordering.Appication.Common.Models
{
    public class OrderDto : IMapFrom<Order>
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        // Address

        public string ShippingAddress { get; set; }

        public string InvoiceAddress { get; set; }

        public string Status { get; set; }
    }
}
using MediatR;
using Ordering.Appication.Common.Models;
using Shared.SeedWork;

namespace Ordering.Appication.Features.V1.Orders.GetOrders
{
    public class GetOrdersQuery : IRequest<ApiResult<List<OrderDto>>>
    {
        public string UserName { get; set; }

        public GetOrdersQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
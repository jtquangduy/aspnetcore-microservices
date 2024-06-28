using Contracts.Common.Interfaces;
using Ordering.Domain.Enities;

namespace Ordering.Appication.Common.Interfaces
{
    public interface IOrderRepository : IRepositoryBaseAsync<Order, long>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
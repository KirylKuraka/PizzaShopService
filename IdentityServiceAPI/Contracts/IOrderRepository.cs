using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetOrdersAsync(OrderParameters parameters, bool trackChanges);
        Task<Order> GetOrderAsync(Guid orderID, bool trackChanges);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}

using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderStatusRepository
    {
        Task<PagedList<OrderStatus>> GetOrderStatusesAsync(OrderStatusParameters parameters, bool trackChanges);
        Task<OrderStatus> GetOrderStatusAsync(Guid orderStatusID, bool trackChanges);
        void CreateOrderStatus(OrderStatus orderStatus);
        void UpdateOrderStatus(OrderStatus orderStatus);
        void DeleteOrderStatus(OrderStatus orderStatus);
    }
}

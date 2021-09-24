using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderStatusRepository : RepositoryBase<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<OrderStatus>> GetOrderStatusesAsync(OrderStatusParameters parameters, bool trackChanges)
        {
            var orderStatuses = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<OrderStatus>.ToPagedList(orderStatuses, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<OrderStatus> GetOrderStatusAsync(Guid orderStatusID, bool trackChanges) =>
            await FindByCondition(c => c.OrderStatusID.Equals(orderStatusID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateOrderStatus(OrderStatus orderStatus) => Create(orderStatus);

        public void UpdateOrderStatus(OrderStatus orderStatus) => Update(orderStatus);

        public void DeleteOrderStatus(OrderStatus orderStatus) => Delete(orderStatus);
    }
}

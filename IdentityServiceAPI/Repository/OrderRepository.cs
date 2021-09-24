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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<Order>> GetOrdersAsync(OrderParameters parameters, bool trackChanges)
        {
            var orders = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<Order>.ToPagedList(orders, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Order> GetOrderAsync(Guid orderID, bool trackChanges) =>
            await FindByCondition(c => c.OrderID.Equals(orderID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateOrder(Order order) => Create(order);

        public void UpdateOrder(Order order) => Update(order);

        public void DeleteOrder(Order order) => Delete(order);
    }
}

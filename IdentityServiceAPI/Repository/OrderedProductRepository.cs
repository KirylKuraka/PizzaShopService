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
    public class OrderedProductRepository : RepositoryBase<OrderedProduct>, IOrderedProductRepository
    {
        public OrderedProductRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<OrderedProduct>> GetOrderedProductsAsync(OrderedProductParameters parameters, bool trackChanges)
        {
            var orderedProducts = await FindAll(trackChanges)
                                .Search(parameters.SearchTerm)
                                .Sort(parameters.OrderBy)
                                .ToListAsync();

            return PagedList<OrderedProduct>.ToPagedList(orderedProducts, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<OrderedProduct> GetOrderedProductAsync(Guid orderedProductID, bool trackChanges) =>
            await FindByCondition(c => c.OrderedProductID.Equals(orderedProductID), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateOrderedProduct(OrderedProduct orderedProduct) => Create(orderedProduct);

        public void UpdateOrderedProduct(OrderedProduct orderedProduct) => Update(orderedProduct);

        public void DeleteOrderedProduct(OrderedProduct orderedProduct) => Delete(orderedProduct);
    }
}

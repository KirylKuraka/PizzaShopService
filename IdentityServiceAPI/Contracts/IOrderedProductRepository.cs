using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderedProductRepository
    {
        Task<PagedList<OrderedProduct>> GetOrderedProductsAsync(OrderedProductParameters parameters, bool trackChanges);
        Task<OrderedProduct> GetOrderedProductAsync(Guid orderedProductID, bool trackChanges);
        void CreateOrderedProduct(OrderedProduct orderedProduct);
        void UpdateOrderedProduct(OrderedProduct orderedProduct);
        void DeleteOrderedProduct(OrderedProduct orderedProduct);
    }
}

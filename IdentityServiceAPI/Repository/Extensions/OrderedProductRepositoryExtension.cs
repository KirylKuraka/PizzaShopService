using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Repository.Extensions
{
    public static class OrderedProductRepositoryExtension
    {
        public static IQueryable<OrderedProduct> Sort(this IQueryable<OrderedProduct> orderedProducts, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return orderedProducts.OrderBy(e => e.ProductName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<OrderedProduct>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return orderedProducts.OrderBy(e => e.ProductName);
            }

            return orderedProducts.OrderBy(orderQuery);
        }

        public static IQueryable<OrderedProduct> Search(this IQueryable<OrderedProduct> orderedProducts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return orderedProducts;
            }

            var propertyInfos = typeof(OrderedProduct).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var orederedProduct in orderedProducts)
            {
                int propertyCount = SearchService.SearchByProperties<OrderedProduct>(searchTerm, propertyInfos, orederedProduct);

                if (propertyCount == propertyInfos.Length)
                {
                    orderedProducts = orderedProducts.Where(e => e.OrderedProductID != orederedProduct.OrderedProductID);
                }
            }

            return orderedProducts;
        }
    }
}

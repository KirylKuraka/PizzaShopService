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
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> Filter(this IQueryable<Product> products, string filterTerm)
        {
            if (string.IsNullOrWhiteSpace(filterTerm))
                return products;

            var lowerCaseTerm = filterTerm.Trim().ToLower();

            return products.Where(e => e.ProductType.ProductTypeName.ToLower().Equals(lowerCaseTerm));
        }

        public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return products.OrderBy(e => e.ProductName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return products.OrderBy(e => e.ProductName);
            }

            return products.OrderBy(orderQuery);
        }

        public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return products;
            }

            var propertyInfos = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var product in products)
            {
                int propertyCount = SearchService.SearchByProperties<Product>(searchTerm, propertyInfos, product);

                if (propertyCount == propertyInfos.Length)
                {
                    products = products.Where(e => e.ProductID != product.ProductID);
                }
            }

            return products;
        }
    }
}

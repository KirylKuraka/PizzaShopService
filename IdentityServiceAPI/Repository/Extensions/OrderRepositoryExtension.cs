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
    public static class OrderRepositoryExtension
    {
        public static IQueryable<Order> Sort(this IQueryable<Order> orders, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return orders.OrderBy(e => e.OrderDate);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Order>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return orders.OrderBy(e => e.OrderDate);
            }

            return orders.OrderBy(orderQuery);
        }

        public static IQueryable<Order> Search(this IQueryable<Order> orders, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return orders;
            }

            var propertyInfos = typeof(Order).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var order in orders)
            {
                int propertyCount = SearchService.SearchByProperties<Order>(searchTerm, propertyInfos, order);

                if (propertyCount == propertyInfos.Length)
                {
                    orders = orders.Where(e => e.OrderID != order.OrderID);
                }
            }

            return orders;
        }
    }
}

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
    public static class OrderStatusRepositoryExtension
    {
        public static IQueryable<OrderStatus> Sort(this IQueryable<OrderStatus> orderStatuses, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return orderStatuses.OrderBy(e => e.OrderStatusName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<OrderStatus>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return orderStatuses.OrderBy(e => e.OrderStatusName);
            }

            return orderStatuses.OrderBy(orderQuery);
        }

        public static IQueryable<OrderStatus> Search(this IQueryable<OrderStatus> orderStatuses, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return orderStatuses;
            }

            var propertyInfos = typeof(OrderStatus).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var orderStatus in orderStatuses)
            {
                int propertyCount = SearchService.SearchByProperties<OrderStatus>(searchTerm, propertyInfos, orderStatus);

                if (propertyCount == propertyInfos.Length)
                {
                    orderStatuses = orderStatuses.Where(e => e.OrderStatusID != orderStatus.OrderStatusID);
                }
            }

            return orderStatuses;
        }
    }
}

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
    public static class DeliveryMethodRepositoryExtension
    {
        public static IQueryable<DeliveryMethod> Sort(this IQueryable<DeliveryMethod> deliveryMethods, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return deliveryMethods.OrderBy(e => e.DeliveryMethodName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return deliveryMethods.OrderBy(e => e.DeliveryMethodName);
            }

            return deliveryMethods.OrderBy(orderQuery);
        }

        public static IQueryable<DeliveryMethod> Search(this IQueryable<DeliveryMethod> deliveryMethods, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return deliveryMethods;
            }

            var propertyInfos = typeof(DeliveryMethod).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var deliveryMethod in deliveryMethods)
            {
                int propertyCount = SearchService.SearchByProperties<DeliveryMethod>(searchTerm, propertyInfos, deliveryMethod);

                if (propertyCount == propertyInfos.Length)
                {
                    deliveryMethods = deliveryMethods.Where(e => e.DeliveryMethodID != deliveryMethod.DeliveryMethodID);
                }
            }

            return deliveryMethods;
        }
    }
}

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
    public static class PaymentMethodRepositoryExtension
    {
        public static IQueryable<PaymentMethod> Sort(this IQueryable<PaymentMethod> paymentMethods, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return paymentMethods.OrderBy(e => e.PaymentMethodName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return paymentMethods.OrderBy(e => e.PaymentMethodName);
            }

            return paymentMethods.OrderBy(orderQuery);
        }

        public static IQueryable<PaymentMethod> Search(this IQueryable<PaymentMethod> paymentMethods, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return paymentMethods;
            }

            var propertyInfos = typeof(PaymentMethod).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var paymentMethod in paymentMethods)
            {
                int propertyCount = SearchService.SearchByProperties<PaymentMethod>(searchTerm, propertyInfos, paymentMethod);

                if (propertyCount == propertyInfos.Length)
                {
                    paymentMethods = paymentMethods.Where(e => e.PaymentMethodID != paymentMethod.PaymentMethodID);
                }
            }

            return paymentMethods;
        }
    }
}

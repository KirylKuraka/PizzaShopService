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
    public static class CustomerRepositoryExtension
    {
        public static IQueryable<Customer> Sort(this IQueryable<Customer> customers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return customers.OrderBy(e => e.CustomerName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return customers.OrderBy(e => e.CustomerName);
            }

            return customers.OrderBy(orderQuery);
        }

        public static IQueryable<Customer> Search(this IQueryable<Customer> customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return customers;
            }

            var propertyInfos = typeof(Customer).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var customer in customers)
            {
                int propertyCount = SearchService.SearchByProperties<Customer>(searchTerm, propertyInfos, customer);

                if (propertyCount == propertyInfos.Length)
                {
                    customers = customers.Where(e => e.CustomerID != customer.CustomerID);
                }
            }

            return customers;
        }
    }
}

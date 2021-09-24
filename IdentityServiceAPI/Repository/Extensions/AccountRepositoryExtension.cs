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
    public static class AccountRepositoryExtension
    {
        public static IQueryable<Account> Sort(this IQueryable<Account> accounts, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return accounts.OrderBy(e => e.UserName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Account>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return accounts.OrderBy(e => e.UserName);
            }

            return accounts.OrderBy(orderQuery);
        }

        public static IQueryable<Account> Search(this IQueryable<Account> accounts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return accounts;
            }

            var propertyInfos = typeof(Account).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var account in accounts)
            {
                int propertyCount = SearchService.SearchByProperties<Account>(searchTerm, propertyInfos, account);

                if (propertyCount == propertyInfos.Length)
                {
                    accounts = accounts.Where(e => e.UserID != account.UserID);
                }
            }

            return accounts;
        }
    }
}

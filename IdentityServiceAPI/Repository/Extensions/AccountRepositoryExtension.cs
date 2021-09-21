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
                int propertyCount = 0;
                foreach (var property in propertyInfos)
                {
                    object value = property.GetValue(account, null);
                    if (value != null)
                    {
                        Regex regex = new Regex("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$");
                        Match match = regex.Match(value.ToString());
                        if (!regex.Match(value.ToString()).Success)
                        {
                            if (!value.ToString().ToLower().Contains(searchTerm.Trim().ToLower()))
                            {
                                propertyCount++;
                            }
                        }
                        else
                        {
                            propertyCount++;
                        }
                    }
                    else
                    {
                        propertyCount++;
                    }
                }

                if (propertyCount == propertyInfos.Length)
                {
                    accounts = accounts.Where(e => e.UserID != account.UserID);
                }
            }

            return accounts;

            //return accounts.Where(e => e.UserName.ToLower().Contains(searchTerm.Trim().ToLower()));
        }
    }
}

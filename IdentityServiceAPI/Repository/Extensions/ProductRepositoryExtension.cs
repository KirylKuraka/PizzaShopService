using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

       /* public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return products.Where(e => e.ProductType.ProductTypeName.ToLower().Contains(lowerCaseTerm));
        }*/
    }
}

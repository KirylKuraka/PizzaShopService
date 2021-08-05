using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<List<Product>> GetProductsAsync(ProductParameters parameters, bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                                .Include(e => e.ProductType)
                                .Filter(parameters.FilterTerm)
                                .OrderBy(c => c.ProductName)
                                .ToListAsync();

            //return PagedList<Product>.ToPagedList(products, parameters.PageNumber, parameters.PageSize);
            return products;
        }

        public async Task<Product> GetProductAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.ProductID.Equals(id), trackChanges)
                .Include(e => e.ProductType)
                .SingleOrDefaultAsync();

        public void CreateProduct(Product product) => Create(product);

        public void UpdateProduct(Product product) => Update(product);

        public void DeleteProduct(Product product) => Delete(product);
    }
}

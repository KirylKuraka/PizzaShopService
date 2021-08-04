using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductTypeRepository : RepositoryBase<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<List<ProductType>> GetProductTypesAsync(ProductTypeParameters parameters, bool trackChanges)
        {
            var productTypes = await FindAll(trackChanges)
                                .OrderBy(c => c.ProductTypeName)
                                .ToListAsync();

            //return PagedList<ProductType>.ToPagedList(productTypes, parameters.PageNumber, parameters.PageSize);
            return productTypes;
        }

        public async Task<ProductType> GetProductTypeAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.ProductTypeID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateProductType(ProductType productType) => Create(productType);

        public void UpdateProductType(ProductType productType) => Update(productType);

        public void DeleteProductType(ProductType productType) => Delete(productType);
    }
}

using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductTypeRepository
    {
        Task<List<ProductType>> GetProductTypesAsync(ProductTypeParameters parameters, bool trackChanges);
        Task<ProductType> GetProductTypeAsync(Guid id, bool trackChanges);
        void CreateProductType(ProductType productType);
        void UpdateProductType(ProductType productType);
        void DeleteProductType(ProductType productType);
    }
}

using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("productTypes")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProductTypesController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductTypesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of all ProductTypes
        /// </summary>
        /// <param name="parameters">Input parameters such as page size and page number</param>
        /// <returns>The list of ProductTypes</returns>
        [HttpGet(Name = "GetProductTypes")]
        public async Task<IEnumerable<ProductType>> GetProductTypes([FromQuery] ProductTypeParameters parameters)
        {
            var productTypes = await _repository.ProductTypeRepository.GetProductTypesAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(productTypes.MetaData));

            return productTypes;
        }

        /// <summary>
        /// Get the ProductType record
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <returns>ProductType record</returns>
        [HttpGet("{id}", Name = "ProductTypeById")]
        public async Task<ProductType> GetProductType(Guid id)
        {
            var productType = await _repository.ProductTypeRepository.GetProductTypeAsync(id, trackChanges: false);
            if (productType == null)
            {
                _logger.LogInfo($"Prodcut type with id: {id} doesn't exist in the database.");
                return null;
            }
            else
            {
                return productType;
            }
        }

        /// <summary>
        /// Create new ProdcutType record
        /// </summary>
        /// <param name="productType">ProductType for creation</param>
        /// <returns>String message about execution status</returns>
        [HttpPost]
        public async Task<string> CreateProductType([FromBody] ProductType productType)
        {
            try
            {
                var productTypeEntity = _mapper.Map<ProductType>(productType);
                _repository.ProductTypeRepository.CreateProductType(productTypeEntity);
                await _repository.SaveAsync();

                return "Product type was created";
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                return $"{e.Message}";
            }
        }

        /// <summary>
        /// Delete ProductType record
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <returns>String message about execution status</returns>
        [HttpDelete("{id}")]
        public async Task<string> DeleteProductType(Guid id)
        {
            var productType = await _repository.ProductTypeRepository.GetProductTypeAsync(id, false);

            if (productType == null)
            {
                _logger.LogInfo($"Product type with id: {id} doesn't exist in database");
                return $"Product type with id: {id} doesn't exist in database";
            }

            _repository.ProductTypeRepository.DeleteProductType(productType);

            await _repository.SaveAsync();

            return "Prodcut type was deleted";
        }

        /// <summary>
        /// Update ProductType record
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <param name="productType">ProductType data</param>
        /// <returns>String message about execution status</returns>
        [HttpPut("{id}")]
        public async Task<string> UpdateProductType(Guid id, [FromBody] ProductType productType)
        {
            if (await _repository.ProductTypeRepository.GetProductTypeAsync(id, trackChanges: false) != null)
            {
                _repository.ProductTypeRepository.UpdateProductType(productType);

                await _repository.SaveAsync();

                return "Prodcut type was updated";
            }
            else
            {
                _logger.LogInfo($"Product type with id {id} doesn't exist in the database");
                return $"Product type with id {id} doesn't exist in the database";
            }
        }
    }
}

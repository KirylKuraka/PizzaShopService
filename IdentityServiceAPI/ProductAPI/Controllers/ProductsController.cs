using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures.Parameters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("products")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProductsController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of Products
        /// </summary>
        /// <param name="parameters">Input parameters such as page size and page number</param>
        /// <returns>The list of Products</returns>
        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts([FromQuery] ProductParameters parameters)
        {
            var products = await _repository.ProductRepository.GetProductsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return products;
        }

        /// <summary>
        /// Get an Product record by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product record</returns>
        [HttpGet("{id}", Name = "ProductById")]
        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _repository.ProductRepository.GetProductAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return null;
            }
            else
            {
                return product;
            }
        }

        /// <summary>
        /// Create new Product record
        /// </summary>
        /// <param name="product">Product record for creation</param>
        /// <returns>String message about execution status</returns>
        [HttpPost]
        public async Task<string> CreateProduct([FromBody] Product product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                _repository.ProductRepository.CreateProduct(productEntity);
                await _repository.SaveAsync();

                return "Product was created";
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                return $"{e.Message}";
            }
        }

        /// <summary>
        /// Delete Product record
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>String message about execution status</returns>
        [HttpDelete("{id}")]
        public async Task<string> DeleteProduct(Guid id)
        {
            var product = await _repository.ProductRepository.GetProductAsync(id, false);

            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in database");
                return $"Product with id: {id} doesn't exist in database";
            }

            _repository.ProductRepository.DeleteProduct(product);

            await _repository.SaveAsync();

            return "Product was deleted";
        }

        /// <summary>
        /// Update Product record
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product data</param>
        /// <returns>String message about execution status</returns>
        [HttpPut("{id}")]
        public async Task<string> UpdateProduct(Guid id, [FromBody] Product product)
        {
            if (await _repository.ProductRepository.GetProductAsync(id, trackChanges: false) != null)
            {
                _repository.ProductRepository.UpdateProduct(product);

                await _repository.SaveAsync();

                return "Product was updated";
            }
            else
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in database");
                return $"Product with id: {id} doesn't exist in database";
            }
        }
    }
}

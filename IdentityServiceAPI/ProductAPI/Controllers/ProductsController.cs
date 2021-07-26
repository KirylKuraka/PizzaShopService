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

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters parameters)
        {
            var products = await _repository.ProductRepository.GetProductsAsync(parameters, trackChanges: false);

            return Ok(products);
        }

        [HttpGet("{id}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _repository.ProductRepository.GetProductAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var productEntity = _mapper.Map<Product>(product);
            _repository.ProductRepository.CreateProduct(productEntity);
            await _repository.SaveAsync();

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _repository.ProductRepository.GetProductAsync(id, false);

            if (product == null)
            {
                _logger.LogInfo($"Product type with id: {id} doesn't exist in database");
                return NotFound();
            }

            _repository.ProductRepository.DeleteProduct(product);

            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (await _repository.ProductRepository.GetProductAsync(product.ProductID, trackChanges: false) != null)
            {
                _repository.ProductRepository.UpdateProduct(product);

                await _repository.SaveAsync();

                return Ok("Account was updated");
            }
            else
            {
                return NotFound($"Account with id {product.ProductID} doesn't exist in the database");
            }
        }
    }
}

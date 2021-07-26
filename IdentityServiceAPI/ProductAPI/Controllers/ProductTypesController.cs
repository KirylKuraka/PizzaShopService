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

        [HttpGet(Name = "GetProductTypes")]
        public async Task<IActionResult> GetProductTypes([FromQuery] ProductTypeParameters parameters)
        {
            var productTypes = await _repository.ProductTypeRepository.GetProductTypesAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(productTypes.MetaData));

            return Ok(productTypes);
        }

        [HttpGet("{id}", Name = "ProductTypeById")]
        public async Task<IActionResult> GetProductType(Guid id)
        {
            var productType = await _repository.ProductTypeRepository.GetProductTypeAsync(id, trackChanges: false);
            if (productType == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(productType);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductType([FromBody] ProductType productType)
        {
            var productTypeEntity = _mapper.Map<ProductType>(productType);
            _repository.ProductTypeRepository.CreateProductType(productTypeEntity);
            await _repository.SaveAsync();

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(Guid id)
        {
            var productType = await _repository.ProductTypeRepository.GetProductTypeAsync(id, false);

            if (productType == null)
            {
                _logger.LogInfo($"Product type with id: {id} doesn't exist in database");
                return NotFound();
            }

            _repository.ProductTypeRepository.DeleteProductType(productType);

            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType([FromBody] ProductType productType)
        {
            if (await _repository.ProductTypeRepository.GetProductTypeAsync(productType.ProductTypeID, trackChanges: false) != null)
            {
                _repository.ProductTypeRepository.UpdateProductType(productType);

                await _repository.SaveAsync();

                return Ok("Account was updated");
            }
            else
            {
                return NotFound($"Account with id {productType.ProductTypeID} doesn't exist in the database");
            }
        }
    }
}

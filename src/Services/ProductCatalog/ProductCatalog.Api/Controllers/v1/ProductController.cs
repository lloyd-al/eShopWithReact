using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Services.ProductCatalog.Application.DTOs;
using eShopWithReact.Services.ProductCatalog.Core.Entities;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;
using eShopWithReact.Common.Core.Entities.Filters;
using eShopWithReact.Common.Core.Entities.Wrappers;


namespace eShopWithReact.Services.ProductCatalog.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : RootController
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] RequestParameters filter)
        {
            var validFilter = new RequestParameters(filter.PageNumber, filter.PageSize);

            var result = await _repository.Products.GetAll(validFilter);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(result.products);


            var response = new PagedResponse<IEnumerable<ProductDto>>(productsDto, result.totalRecords, validFilter);

            var metadata = new
            {
                response.TotalCount,
                response.PageSize,
                response.CurrentPage,
                response.TotalPages,
                response.HasNext,
                response.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _repository.Products.GetById(id);

            if (product == null)
            {   
                _logger.LogError($"Product with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            var category = await _repository.Categories.GetById(product.Category);
            if (category != null)
                product.CategoryDetail = category;

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(product);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            var products = await _repository.Products.GetByName(name);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(products);
        }


        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category, [FromQuery] RequestParameters filter)
        {
            var validFilter = new RequestParameters(filter.PageNumber, filter.PageSize);

            var result = await _repository.Products.GetByCategory(category, validFilter);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(result.products);


            var response = new PagedResponse<IEnumerable<ProductDto>>(productsDto, result.totalRecords, validFilter);

            var metadata = new
            {
                response.TotalCount,
                response.PageSize,
                response.CurrentPage,
                response.TotalPages,
                response.HasNext,
                response.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.Products.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repository.Products.Update(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.Products.Delete(id));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using eShopWithReact.Common.Core.Interfaces;
using eShopWithReact.Services.ProductCatalog.Application.DTOs;
using eShopWithReact.Services.ProductCatalog.Core.Entities;
using eShopWithReact.Services.ProductCatalog.Core.Interfaces;

namespace eShopWithReact.Services.ProductCatalog.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CategoryController : RootController
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CategoryController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repository.Categories.GetAll();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> GetCategory(string id)
        {
            var category = await _repository.Products.GetById(id);

            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesByName(string name)
        {
            var categories = await _repository.Categories.GetByName(name);
            var categoriesDto = _mapper.Map<IEnumerable<ProductDto>>(categories);
            return Ok(categoriesDto);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            await _repository.Categories.Create(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Category category)
        {
            return Ok(await _repository.Categories.Update(category));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.Categories.Delete(id));
        }
    }
}

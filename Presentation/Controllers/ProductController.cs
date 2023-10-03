using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/categories/{categoryId}/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProductController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(Guid categoryId, [FromQuery]ProductParameters productParameters)
        {
            var products = await _service.ProductService.GetAllProductsAsync(categoryId, productParameters, trackChanges: false);
            return Ok(products);
        }

        [HttpGet("{productId:guid}" , Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(Guid categoryId, Guid productId)
        {
            var product = await _service.ProductService.GetProductAsync(categoryId, productId, trackChanges: false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Guid categoryId,  [FromForm]ProductUpdateCreateDto productDto)
        {
            var product = await _service.ProductService.CreateProductAsync(categoryId, productDto, trackChanges: false);
            return CreatedAtRoute("GetProduct" , new {categoryId, product.ProductId }, product);
        }

        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid categoryId, Guid productId, [FromForm]ProductUpdateCreateDto updateDto)   
        {
            await _service.ProductService.UpdateProductAsync(categoryId, productId, updateDto, categoryTrachkChanges: false, productTrachChages: true);
            return NoContent();
        }
    }
}

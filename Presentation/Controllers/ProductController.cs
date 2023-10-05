using Contract.Service;
using Entity.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

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
            // MaxPriceRange Exception Catch
            if(!(productParameters.MinPrice < productParameters.MaxPrice))
                throw new MaxPriceRangeBadRequestException();

            var pagedResult = await _service.ProductService.GetAllProductsAsync(categoryId, productParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.products);
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
            await _service.ProductService.UpdateProductAsync(categoryId, productId, updateDto, categoryTrachkChanges: false, productTrackChages: true);
            return NoContent();
        }

        [HttpPatch("{productId:guid}")]
        public async Task<IActionResult> PartiallyUpdateProduct(Guid categoryId, Guid productId, [FromBody] JsonPatchDocument<ProductUpdateCreateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _service.ProductService.GetProductForPatchAsync(categoryId, productId, categoryTrackChanges: false, productTrackChages: true);
            patchDoc.ApplyTo(result.productToPatch);

            await _service.ProductService.SaveChangesForPatchAsync(result.productToPatch, result.productEntity);
            return NoContent();
        }

    }
}

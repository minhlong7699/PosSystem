using Contract.Service;
using Entity.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Product;
using Shared.RequestFeatures;
using System.Data;

namespace Presentation.Controllers
{
    /// <summary>
    /// API for Product.
    /// </summary>
    [Route("api/categories/{categoryId}/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProductController(IServiceManager service)
        {
            _service = service;
        }


        /// <summary>
        /// Get all Products in a specific category. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="categoryId">The unique ID of the product category.</param>
        /// <param name="productParameters">Parameters for filtering and pagination.</param>
        /// <returns>
        /// A list of products within the specified category.
        /// </returns>
        /// <remarks>
        /// An example URL:
        /// GET /api/categories/123/products 
        /// Where 123 is the ID of the product category.
        ///</remarks>
        /// <response code="200">Returns a list of products within the specified category.</response>
        /// <response code="400">If the provided price range is invalid (minPrice should be less than maxPrice).</response>
        /// 

        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetProducts(Guid categoryId, [FromQuery] ProductParameters productParameters)
        {

            // MaxPriceRange Exception Catch
            if (!(productParameters.MinPrice < productParameters.MaxPrice))
                throw new MaxPriceRangeBadRequestException();

            var pagedResult = await _service.ProductService.GetAllProductsAsync(categoryId, productParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.products);
        }


        /// <summary>
        /// Get a Product by its unique ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="categoryId">The unique ID of the product category.</param>
        /// <param name="productId">The unique ID of the product to retrieve.</param>
        /// <returns>
        /// The product with the specified ID.
        /// </returns>
        /// <response code="200">Returns the product with the specified ID.</response>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{productId:guid}", Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(Guid categoryId, Guid productId)
        {
            var product = await _service.ProductService.GetProductAsync(categoryId, productId, trackChanges: false);
            return Ok(product);
        }


        /// <summary>
        /// Create a new Product within a specific category. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categoryId">The unique ID of the product category.</param>
        /// <param name="productDto">The data for the new product.</param>
        /// <returns>
        /// The newly created product.
        /// </returns>
        /// <response code="201">Returns the newly created product.</response>
        /// 
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Guid categoryId, [FromForm] ProductUpdateCreateDto productDto)
        {
            var product = await _service.ProductService.CreateProductAsync(categoryId, productDto, trackChanges: false);
            return CreatedAtRoute("GetProduct", new { categoryId, product.ProductId }, product);
        }


        /// <summary>
        /// Update an existing Product within a specific category. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categoryId">The unique ID of the product category.</param>
        /// <param name="productId">The unique ID of the product to update.</param>
        /// <param name="updateDto">The updated data for the product.</param>
        /// <returns>
        /// No content if the update is successful.
        /// </returns>
        /// <response code="204">No content if the update is successful.</response>
        /// 
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid categoryId, Guid productId, [FromForm] ProductUpdateCreateDto updateDto)
        {
            await _service.ProductService.UpdateProductAsync(categoryId, productId, updateDto, categoryTrachkChanges: false, productTrackChages: true);
            return NoContent();
        }


        /// <summary>
        /// Partially update an existing Product within a specific category. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categoryId">The unique ID of the product category.</param>
        /// <param name="productId">The unique ID of the product to update.</param>
        /// <param name="patchDoc">The JSON Patch document for partial updates.</param>
        /// <returns>
        /// No content if the update is successful.
        /// </returns>
        /// <response code="204">No content if the update is successful.</response>
        /// 

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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



        /// <summary>
        /// Delete a Product by its unique ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categoryId">The unique ID of the product category.</param>
        /// <param name="productId">The unique ID of the product to remove.</param>
        /// <returns>
        /// The product with the specified ID.
        /// </returns>
        /// <response code="204">No content if the update is successful.</response>
        /// 
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid categoryId, Guid productId)
        {
            await _service.ProductService.DeleteProductAsync(categoryId, productId, trackChanges: false);
            return NoContent();
        }

    }
}

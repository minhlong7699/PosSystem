using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Category;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    /// <summary>
    /// API for managed Category.
    /// </summary>
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CategoryController(IServiceManager service)
        {
            _service = service;
        }


        /// <summary>
        /// Get a list of all categories. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="categoryParamaters">Query parameters for pagination and filtering.</param>
        /// <returns>
        /// A paginated list of categories.
        /// </returns>
        /// <response code="200">Returns a paginated list of categories.</response>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories([FromQuery]CategoryParameters categoryParamaters)
        {
            var pagedResult = await _service.CategoryService.GetAllCategoriesAsync(categoryParamaters ,trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.categories);
        }


        /// <summary>
        /// Get a specific category by its unique identifier. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="id">The unique ID of the category.</param>
        /// <returns>
        /// The category with the specified ID.
        /// </returns>
        /// <response code="200">Returns the category with the specified ID.</response>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{id:guid}", Name = "CategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false);
            return Ok(category);
        }

        /// <summary>
        /// Create a new category. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categorydto">Data for creating the new category.</param>
        /// <returns>
        /// The created category.
        /// </returns>
        /// <response code="201">Returns the newly created category.</response>
        /// 
        [Authorize(Roles = "Administrator")]        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryUpdateCreateDto categorydto)
        {
            if (categorydto is null)         
                return BadRequest("CategoryUpdateCreateDto object is null");

            var createdCategory = await _service.CategoryService.CreateCategoryAsync(categorydto);
            return CreatedAtRoute("CategoryById", new { id = createdCategory.CategoryId }, createdCategory);
        }

        /// <summary>
        /// Update an existing category. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categoryId">The unique ID of the category to update.</param>
        /// <param name="categoryUpdate">Data for updating the category.</param>
        /// <returns>
        /// No content if the update is successful.
        /// </returns>
        /// <response code="204">Returns no content if the update is successful.</response>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{categoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody]CategoryUpdateCreateDto categoryUpdate)
        {
            await _service.CategoryService.UpdateCategoryAsync(categoryId, categoryUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing category. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="categoryId">The unique ID of the category to delete.</param>
        /// <returns>
        /// No content if the deletion is successful.
        /// </returns>
        /// <response code="204">Returns no content if the deletion is successful.</response>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategoy(Guid categoryId)
        {
            await _service.CategoryService.DeleteCategoryAsync(categoryId, trackChanges: false);
            return NoContent();
        }
    }
}

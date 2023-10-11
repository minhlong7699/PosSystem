using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery]CategoryParameters categoryParamaters)
        {
            var pagedResult = await _service.CategoryService.GetAllCategoriesAsync(categoryParamaters ,trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.categories);
        }

        [HttpGet("{id:guid}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryUpdateCreateDto categorydto)
        {
            if (categorydto is null)         
                return BadRequest("CategoryUpdateCreateDto object is null");

            var createdCategory = await _service.CategoryService.CreateCategoryAsync(categorydto);
            return CreatedAtRoute("CategoryById", new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody]CategoryUpdateCreateDto categoryUpdate)
        {
            await _service.CategoryService.UpdateCategoryAsync(categoryId, categoryUpdate, trackChanges: true);
            return NoContent();
        }
    }
}

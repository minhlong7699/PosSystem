using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

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
        public IActionResult GetCategories()
        {
            var categories = _service.CategoryService.GetAllCategories(trackChanges: false);
            return Ok(categories);
        }

        [HttpGet("{id:guid}", Name = "CategoryById")]
        public IActionResult GetCategory(Guid id)
        {
            var category = _service.CategoryService.GetCategory(id, trackChanges: false);
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryUpdateCreateDto categorydto)
        {
            if (categorydto is null)
            {
                return BadRequest("CategoryUpdateCreateDto object is null");
            }

            var createdCategory = _service.CategoryService.CreateCategory(categorydto);
            return CreatedAtRoute("CategoryById", new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategory(Guid categoryId, [FromForm]CategoryUpdateCreateDto categoryUpdate)
        {
            _service.CategoryService.UpdateCategory(categoryId, categoryUpdate, trackChanges: true);
            return NoContent();
        }
    }
}

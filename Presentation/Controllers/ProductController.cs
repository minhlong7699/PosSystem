using Contract.Service;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllProducts(Guid categoryId)
        {
            var products =_service.ProductService.GetAllProducts(categoryId, trackChanges: false);
            return Ok(products);
        }
    }
}

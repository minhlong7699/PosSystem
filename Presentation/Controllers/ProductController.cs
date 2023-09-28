using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
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

        [HttpGet("{productId:guid}" , Name = "GetProduct")]
        public IActionResult GetProduct(Guid categoryId, Guid productId)
        {
            var product = _service.ProductService.GetProduct(categoryId, productId, trackChanges: false);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Guid categoryId,  [FromBody]ProductUpdateCreateDto productDto)
        {
            var product = _service.ProductService.CreateProduct(categoryId, productDto, trackChanges: false);
            return CreatedAtRoute("GetProduct" , new {categoryId, product.ProductId }, product);
        }
    }
}

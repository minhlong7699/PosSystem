using Contract.Service;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _service;
        public OrderController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery]OrderParameters orderParameters)
        {
            var orders = await _service.OrderService.GetAllOrderAsync(orderParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(orders.metaData));
            return Ok(orders.orders);
        }

        [HttpGet("{orderId:guid}" , Name = "getOrder")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await _service.OrderService.GetOrderAsync(orderId, trackChanges: false);
            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm]OrderCreateUpdateDto orderCreate)
        {
            var order = await _service.OrderService.CreateOrderAsync(orderCreate, trackChanges: false);
            return CreatedAtRoute("getOrder", new {order.OrderId}, order);
         }

    }
}

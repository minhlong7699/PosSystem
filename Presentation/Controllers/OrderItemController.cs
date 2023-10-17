using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId}/orderitems")]
    public class OrderItemController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrderItemController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems(Guid orderId, [FromQuery]OrderItemsParameters orderItemsParameters)
        {
            var pagedResult = await _service.OrderItemService.GetAllOrderItemsAsync(orderId, orderItemsParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.orderItems);
        }

        [HttpGet("{orderItemId:guid}", Name = "getOrderItem")]
        public async Task<IActionResult> GetOrderItem(Guid orderId, Guid orderItemId)
        {
            var orderItem = await _service.OrderItemService.GetOrderItemAsync(orderId, orderItemId, trackChanges: false);
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItems(Guid orderId, OrderItemCreateUpdateDto orderItemCreate)
        {
            var orderItem = await _service.OrderItemService.CreateOrderItemAsync(orderId, orderItemCreate, trackChanges: true);
            return CreatedAtRoute("getOrderItem", new { orderId, orderItemId = orderItem.OrderItemsId }, orderItem);
        }

        [HttpPut("{orderItemId:guid}")]
        public async Task<IActionResult> UpdateOrderItem(Guid orderId, Guid orderItemId, OrderItemUpdateDto orderItemUpdate)
        {
            await _service.OrderItemService.UpdateOrderItemAsync(orderId, orderItemId, orderItemUpdate, trackChanges: true);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, Guid orderItemId)
        {
            await _service.OrderItemService.DeleteOrderItemsAsync(orderId, orderItemId, trackChanges: false);
            return NoContent();
        }
    }
}

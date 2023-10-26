using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.OrderItem;
using Shared.RequestFeatures;
using System.Data;

namespace Presentation.Controllers
{

    /// <summary>
    /// API for managing order items within an order.
    /// </summary>
    [ApiController]
    [Route("api/orders/{orderId}/orderitems")]
    public class OrderItemController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrderItemController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all order items within an order with optional filtering. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="orderId">The unique ID of the order.</param>
        /// <param name="orderItemsParameters">Parameters for filtering and pagination.</param>
        /// <returns>
        /// Returns a collection of order items within the specified order.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderItems(Guid orderId, [FromQuery]OrderItemsParameters orderItemsParameters)
        {
            var pagedResult = await _service.OrderItemService.GetAllOrderItemsAsync(orderId, orderItemsParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.orderItems);
        }

        /// <summary>
        /// Get a specific order item within an order by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="orderId">The unique ID of the order.</param>
        /// <param name="orderItemId">The unique ID of the order item to retrieve.</param>
        /// <returns>
        /// Returns the requested order item within the specified order.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{orderItemId:guid}", Name = "getOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderItem(Guid orderId, Guid orderItemId)
        {
            var orderItem = await _service.OrderItemService.GetOrderItemAsync(orderId, orderItemId, trackChanges: false);
            return Ok(orderItem);
        }

        /// <summary>
        /// Create a new order item within an order. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="orderId">The unique ID of the order to add the order item to.</param>
        /// <param name="orderItemCreate">Data for creating a new order item.</param>
        /// <returns>
        /// Returns the newly created order item within the specified order.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrderItems(Guid orderId, OrderItemCreateUpdateDto orderItemCreate)
        {
            var orderItem = await _service.OrderItemService.CreateOrderItemAsync(orderId, orderItemCreate, trackChanges: true);
            return CreatedAtRoute("getOrderItem", new { orderId, orderItemId = orderItem.OrderItemsId }, orderItem);
        }

        /// <summary>
        /// Update an existing order item within an order. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="orderId">The unique ID of the order containing the order item.</param>
        /// <param name="orderItemId">The unique ID of the order item to update.</param>
        /// <param name="orderItemUpdate">Data for updating the order item.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{orderItemId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrderItem(Guid orderId, Guid orderItemId, OrderItemUpdateDto orderItemUpdate)
        {
            await _service.OrderItemService.UpdateOrderItemAsync(orderId, orderItemId, orderItemUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing order item within an order. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="orderId">The unique ID of the order containing the order item.</param>
        /// <param name="orderItemId">The unique ID of the order item to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, Guid orderItemId)
        {
            await _service.OrderItemService.DeleteOrderItemsAsync(orderId, orderItemId, trackChanges: false);
            return NoContent();
        }
    }
}

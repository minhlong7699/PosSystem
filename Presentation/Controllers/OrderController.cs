using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Order;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
/// <summary>
/// Controller for managing orders.
/// </summary>
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _service;

        /// <summary>
        /// Constructor for the OrderController.
        /// </summary>
        /// <param name="service">The service manager for handling orders.</param>
        public OrderController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a list of orders. This API requires authentication with the 'User' role.
        /// </summary>
        /// <param name="orderParameters">Parameters for filtering and pagination.</param>
        /// <returns>A list of orders.</returns>
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetOrders([FromQuery] OrderParameters orderParameters)
        {
            var orders = await _service.OrderService.GetAllOrderAsync(orderParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(orders.metaData));
            return Ok(orders.orders);
        }

        /// <summary>
        /// Get a specific order by its ID. This API requires authentication with the 'User' role.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve.</param>
        /// <returns>The requested order.</returns>
        [Authorize(Roles = "User")]
        [HttpGet("{orderId:guid}", Name = "getOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await _service.OrderService.GetOrderAsync(orderId, trackChanges: false);
            return Ok(order);
        }

        /// <summary>
        /// Create a new order. This API requires authentication with the 'Administrator' role.
        /// </summary>
        /// <param name="orderCreate">Data for creating the new order.</param>
        /// <returns>The created order.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromForm] OrderCreateUpdateDto orderCreate)
        {
            var order = await _service.OrderService.CreateOrderAsync(orderCreate, trackChanges: true);
            return CreatedAtRoute("getOrder", new { order.OrderId }, order);
        }

        /// <summary>
        /// Update an existing order. This API requires authentication with the 'Administrator' role.
        /// </summary>
        /// <param name="orderId">The ID of the order to update.</param>
        /// <param name="orderUpdate">Data for updating the order.</param>
        /// <returns>No content upon successful update.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromForm] OrderCreateUpdateDto orderUpdate)
        {
            await _service.OrderService.UpdateOrderAsync(orderId, orderUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an order. This API requires authentication with the 'Administrator' role.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        /// <returns>No content upon successful deletion.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _service.OrderService.DeleteOrderAsync(orderId, trackChanges: false);
            return NoContent();
        }
    }
}

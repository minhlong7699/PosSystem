using Contract.Service;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Promotion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    /// <summary>
    /// API for managing promotions.
    /// </summary>
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IServiceManager _services;
        public PromotionController(IServiceManager services)
        {
            _services = services;
        }

        /// <summary>
        /// Get all promotions. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="promotionParamaters">Parameters for filtering and paging promotions.</param>
        /// <returns>
        /// Returns a collection of all promotions.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromotions([FromQuery]PromotionParamaters promotionParamaters)
        {
            var pagedResult = await _services.PromotionService.GetAllPromotionsAsync(promotionParamaters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metadata));
            return Ok(pagedResult.promotions);
        }

        /// <summary>
        /// Get a specific promotion by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="promotionId">The unique ID of the promotion to retrieve.</param>
        /// <returns>
        /// Returns the requested promotion.
        /// Returns status code 200 (OK).
        /// </returns>
        ///
        [Authorize(Roles = "User")]
        [HttpGet("{promotionId:guid}", Name = "getPromotion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromotion(Guid promotionId)
        {
            var promotion = await _services.PromotionService.GetPromotionAsync(promotionId, trackChanges: false);
            return Ok(promotion);
        }

        /// <summary>
        /// Create a new promotion. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="updateCreateDto">Data for creating a new promotion.</param>
        /// <returns>
        /// Returns the newly created promotion.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePromotion(PromotionUpdateCreateDto updateCreateDto)
        {
            var promotion = await _services.PromotionService.CreatepromotionAsync(updateCreateDto);
            return CreatedAtRoute("getPromotion", new { promotion.PromotionId }, promotion);
        }

        /// <summary>
        /// Update an existing promotion by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="promotionId">The unique ID of the promotion to update.</param>
        /// <param name="promotionUpdate">Data for updating the promotion.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{promotionId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePromotion(Guid promotionId, PromotionUpdateCreateDto promotionUpdate)
        {
            await _services.PromotionService.UpdatePromotionAsync(promotionId, promotionUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing promotion by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="promotionId">The unique ID of the promotion to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePromotion(Guid promotionId)
        {
            await _services.PromotionService.DeletepromotionAsync(promotionId, trackChanges: false);
            return NoContent();
        }
    }
}

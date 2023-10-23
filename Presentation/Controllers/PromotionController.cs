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
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IServiceManager _services;
        public PromotionController(IServiceManager services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetPromotions()
        {
            var promotions = await _services.PromotionService.GetAllPromotionsAsync(trackChanges: false);
            return Ok(promotions);
        }


        [HttpGet("{promotionId:guid}", Name = "getPromotion")]
        public async Task<IActionResult> GetPromotion(Guid promotionId)
        {
            var promotion = await _services.PromotionService.GetPromotionAsync(promotionId, trackChanges: false);
            return Ok(promotion);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromotion(PromotionUpdateCreateDto updateCreateDto)
        {
            var promotion = await _services.PromotionService.CreatepromotionAsync(updateCreateDto);
            return CreatedAtRoute("getPromotion", new { promotion.PromotionId }, promotion);
        }

        [HttpPut("{promotionId:guid}")]
        public async Task<IActionResult> UpdatePromotion(Guid promotionId, PromotionUpdateCreateDto promotionUpdate)
        {
            await _services.PromotionService.UpdatePromotionAsync(promotionId, promotionUpdate, trackChanges: true);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePromotion(Guid promotionId)
        {
            await _services.PromotionService.DeletepromotionAsync(promotionId, trackChanges: false);
            return NoContent();
        }
    }
}

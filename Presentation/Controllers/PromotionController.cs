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
        public IActionResult GetAllPromotions()
        {
            var promotions = _services.PromotionService.GetAllPromotions(trackChanges: false);
            return Ok(promotions);
        }


        [HttpGet("{promotionId:guid}")]
        public IActionResult GetPromotion(Guid promotionId)
        {
            var promotion = _services.PromotionService.GetPromotion(promotionId, trackChanges: false);
            return Ok(promotion);
        }
    }
}

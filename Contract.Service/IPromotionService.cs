using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IPromotionService
    {
        IEnumerable<PromotionDto> GetAllPromotions(bool trackChanges);
        PromotionDto GetPromotion(Guid promotionId,bool trackChanges);
    }
}

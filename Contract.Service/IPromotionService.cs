using MailKit;
using Shared.DataTransferObjects.Promotion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IPromotionService
    {
        Task<(IEnumerable<PromotionDto> promotions, MetaData metadata)> GetAllPromotionsAsync(PromotionParamaters promotionParamaters,bool trackChanges);
        Task<PromotionDto> GetPromotionAsync(Guid promotionId,bool trackChanges);
        Task<PromotionDto> CreatepromotionAsync(PromotionUpdateCreateDto promotionCreate);
        Task UpdatePromotionAsync(Guid promotionId, PromotionUpdateCreateDto promotionUpdate, bool trackChanges);
        Task DeletepromotionAsync(Guid promotionId, bool trackChanges);       
    }
}

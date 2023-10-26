using Entity.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IPromotionRepository
    {
        Task<PagedList<Promotion>> GetAllPromotionsAsync(PromotionParamaters promotionParamaters , bool trackChanges);
        Task<Promotion> GetPromotionAsync(Guid? promotionId ,bool trackChanges);
        void CreatePromotion(Promotion promotion);
        void DeletePromotion(Promotion promotion);
    }
}

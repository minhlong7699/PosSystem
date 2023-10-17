using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync(bool trackChanges);
        Task<Promotion> GetPromotionAsync(Guid? promotionId ,bool trackChanges);
        void CreatePromotion(Promotion promotion);
    }
}

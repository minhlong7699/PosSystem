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
        IEnumerable<Promotion> GetAllPromotions(bool trackChanges);
        Promotion GetPromotion(Guid promotionId ,bool trackChanges);
    }
}

using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PromotionRepository : RepositoryBase<Promotion>, IPromotionRepository
    {
        public PromotionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Promotion> GetAllPromotions(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c => c.PromotionName).ToList();
        }

        public Promotion GetPromotion(Guid promotionId, bool trackChanges)
        {
            return FindByConditon(e => e.PromotionId.Equals(promotionId), trackChanges).SingleOrDefault();
        }
    }
}

using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
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

        public void CreatePromotion(Promotion promotion)
        {
            Create(promotion);
        }

        public void DeletePromotion(Promotion promotion)
        {
            Delete(promotion);
        }

        public async Task<PagedList<Promotion>> GetAllPromotionsAsync(PromotionParamaters promotionParamaters,bool trackChanges)
        {
            var product = await FindAll(trackChanges)
                .FilterPromotion(promotionParamaters.StartDate, promotionParamaters.EndDate)
                .SearchPromotion(promotionParamaters.SearchTerm)
                .SortPromotion(promotionParamaters.orderBy)
                .ToListAsync();
            return PagedList<Promotion>.ToPagedList(product, promotionParamaters.pageNumber, promotionParamaters.pageSize);
        }

        public async Task<Promotion> GetPromotionAsync(Guid? promotionId, bool trackChanges)
        {
            return await FindByConditon(e => e.PromotionId.Equals(promotionId), trackChanges).SingleOrDefaultAsync();
        }
    }
}

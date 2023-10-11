using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class PromotionNotFoundException : NotFoundException
    {
        public PromotionNotFoundException(Guid? promotionId) : base($"Promotion with id: {promotionId} doesn't exist in the database.")
        {
        }
    }
}

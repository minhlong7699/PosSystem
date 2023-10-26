using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Promotion
{
    public record PromotionDto(Guid PromotionId, string PromotionName, string PromotionDescription, DateTime StartDate, DateTime EndDate, float DisCountPercent);
}

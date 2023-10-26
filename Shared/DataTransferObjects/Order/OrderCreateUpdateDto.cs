using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Processing")]
        Processing,
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "Canceled")]
        Canceled
    }

    public record OrderCreateUpdateDto(string? OrderType, OrderStatus Status, Guid PaymentId, Guid TableId, Guid TaxId, Guid? PromotionId);
}

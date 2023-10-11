using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record TableDto(Guid TableID, string Name, string TableDescription, bool IsOccupied, bool IsActive);
}

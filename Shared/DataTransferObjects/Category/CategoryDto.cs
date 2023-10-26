using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Category
{
    public record CategoryDto(Guid CategoryId,
                              string CategoryName,
                              string CategoryDescription);
}

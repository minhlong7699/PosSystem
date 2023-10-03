using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CategoryUpdateCreateDto(
        [Required(ErrorMessage = "Category Name is required field")]
        string CategoryName,
        string CategoryDescription,
        bool IsActive);
}

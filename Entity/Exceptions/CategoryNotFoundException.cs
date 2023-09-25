using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid CategoryId) : base($"The category with id: {CategoryId} doesn't exist in the database.")
        {
        }
    }
}

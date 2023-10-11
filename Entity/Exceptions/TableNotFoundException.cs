using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class TableNotFoundException : NotFoundException
    {
        public TableNotFoundException(Guid tableId) : base($"The table with id: {tableId} doesn't exist in the database.")
        {
        }
    }
}

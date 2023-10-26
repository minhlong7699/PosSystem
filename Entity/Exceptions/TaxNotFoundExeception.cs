using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class TaxNotFoundExeception : NotFoundException
    {
        public TaxNotFoundExeception(Guid taxId) : base($"The Tax with id: {taxId} doesn't exist in the database.")
        {
        }
    }
}

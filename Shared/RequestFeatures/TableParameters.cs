using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class TableParameters : RequestParameters
    {
        public  bool IsOccupied { get; set; }
        public bool IsActive { get; set; } = true;

        public string? SearchTerm { get; set; }
    }
}

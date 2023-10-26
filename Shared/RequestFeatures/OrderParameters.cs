using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class OrderParameters : RequestParameters
    {
        public string? OrderType { get; set; }

        public string? Status { get; set; }

        public bool IsActive { get; set; } = true;          
        public string? TableId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {

        // uint Unsigned 32-bit integer (0 to 4,294,967,295)
        public uint MinPrice {  get; set; }
        public uint MaxPrice { get; set; } = int.MaxValue;
        public bool IsActive { get; set; } = true;

        public string? SearchTerm { get; set; }

    }
}

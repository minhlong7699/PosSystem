using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPage = 50;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int pageSize { get { return _pageSize; } set { _pageSize = (value > maxPage) ? maxPage : value; } }
    }
}

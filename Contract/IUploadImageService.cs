using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IUploadImageService
    {
        string GetImageUrl(IFormFile file, string folder);
    }
}

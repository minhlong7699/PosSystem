using Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExtensionServices
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string GetImageUrl(IFormFile file, string folder)
        {
            if (file != null && file.Length > 0)
            {
                var uniqueName = CreateUniqueFileName(file.FileName);
                // create Url Path

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath,"Images", folder);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            return $"Images/{folder}/{uniqueName}";
            }
            throw new ArgumentNullException(nameof(file));
        }

        private string CreateUniqueFileName(string fileName)
        {
            var postfix = Guid.NewGuid().ToString();
            string uniqueName = $"{postfix}-{fileName}";
            return uniqueName;
        }
    }
}

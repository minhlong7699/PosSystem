using Contract;
using Microsoft.AspNetCore.Hosting;

namespace PosSystem.Extensions
{
    public class WebRootPathProvider : IWebRootPathProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WebRootPathProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string GetRootPath()
        {
            return _webHostEnvironment.WebRootPath;
        }
    }
}

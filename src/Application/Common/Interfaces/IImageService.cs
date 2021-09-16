

using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IImageService
    {
        public string GetImagePath(string image);
        public string UploadImage(IFormFile image);
        public void DeleteImage(string image);
        public bool ImageExists(string image);
    }
}

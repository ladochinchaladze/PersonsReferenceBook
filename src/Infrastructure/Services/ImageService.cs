using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Application.Common.Interfaces;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetImagePath(string image)
        {
            if (string.IsNullOrEmpty(image)) return null;

            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            string imagePath = Path.Combine(folder, image);


            if (!File.Exists(imagePath))
            {
                return null;
            }

            return imagePath;
        }

        public string UploadImage(IFormFile image)
        {

            if (image == null) return string.Empty;

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public void DeleteImage(string image)
        {

            if (string.IsNullOrEmpty(image)) return;

            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            string imagePath = Path.Combine(folder, image);


            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

        }

        public bool ImageExists(string image)
        {
            if (string.IsNullOrEmpty(image)) return false;

            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            string imagePath = Path.Combine(folder, image);


            return File.Exists(imagePath);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace Services
{
    public class ImageService : IImageService
    {
        private readonly IImageConfig config;

        public ImageService(IImageConfig config)
        {
            this.config = config;
        }

        public Image Resize(Image image, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }

            return newImage;
        }

        public Image CropMaxSquare(Image image)
        {
            var width = image.Width > image.Height ? image.Height : image.Width;
            var height = width;

            var newImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(newImage))
                g.DrawImage(
                    image,
                    new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, width, height),
                    GraphicsUnit.Pixel
                );
            return newImage;
        }

        public string Save(IFormFile image)
        {         
            var cropped = CropMaxSquare(Image.FromStream(image.OpenReadStream()));
            var resized = Resize(cropped, 500, 500);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(config.WeebRoot, "Images/Events");

            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, fileName);
            resized.Save(filePath);
            
            return fileName;
        }     
    }
}

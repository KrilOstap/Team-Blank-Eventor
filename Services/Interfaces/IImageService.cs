using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace Services.Interfaces
{
    public interface IImageService
    {
        string Save(IFormFile file);
        Image Resize(Image image, int width, int height);
        Image CropMaxSquare(Image image);
        void Delete(string fileName);
    }
}

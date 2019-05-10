using Moq;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Eventor.Tests
{
    public class ImageServiceTests
    {
        private readonly IImageConfig Config;

        public ImageServiceTests(IImageConfig config)
        {
            this.Config = config;
        }

        [Fact]
        public void Deleteest()
        {
            var config = new Mock<IImageConfig>();
            config.Setup(c => c.WeebRoot).Returns(Config.WeebRoot);

            var service = new ImageService(config.Object);

            service.Delete("Image.png");

            config.Verify(c => c.WeebRoot, Times.Once);
        }
    }
}

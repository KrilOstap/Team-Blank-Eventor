using AutoMapper;
using DataAccess.Data.Interfaces;
using Eventor.Data.Entities;
using Moq;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Eventor.Tests
{
    public class UserServiceTests
    {
        public IMapper Mapper { get; set; }

        public UserServiceTests()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserDTO>();
            })
            .CreateMapper();
        }      

        [Fact]
        public void UpdateInformationTest()
        {
            var repository = new Mock<IRepository<ApplicationUser>>();
            repository.Setup(r => r.Update(It.IsAny<ApplicationUser>()));
            ApplicationUserService service = new ApplicationUserService(repository.Object, Mapper);

            service.UpdateInformation(new ApplicationUser { Id = "1" });

            repository.Verify(r => r.Update(It.IsAny<ApplicationUser>()), Times.Once);
            repository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void GetByIdTest()
        {
            var expected = new ApplicationUserDTO
            {
                Id = "1",
                FirstName = "Ostap"
            };

            var repository = new Mock<IRepository<ApplicationUser>>();
            repository.Setup(r => r.GetById(expected.Id)).Returns(new ApplicationUser { Id = "1"});
            ApplicationUserService service = new ApplicationUserService(repository.Object, Mapper);

            var actual = service.GetById(expected.Id);

            Assert.Equal(expected.Id, actual.Id);
        }

    }
}

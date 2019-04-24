using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Data.Interfaces;
using DataAccess.Data.Repositories;
using DataAccess.Entities;
using Eventor.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Eventor.Test
{
    public class SubscriptionTests
    {     
        [Fact]
        public void GetByIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestSubscription").Options;

            var context = new ApplicationDbContext(options);
            context.Events.Add(new Event { Id = "1", Title = "sub 1" });
            context.Events.Add(new Event { Id = "2", Title = "sub 2" });
            context.SaveChanges();

            var repository = new EventRepository(context);

            var actual = repository.GetById("1");
            var actualForNull = repository.GetById("1000");

            var expected = new Event { Id = "1", Date = new DateTime(2020, 10, 10) };

            Assert.Equal(actual.Id, expected.Id);
            Assert.Null(actualForNull);            
        }

        [Fact]
        public void GetEventsTest()
        {
            var repository = new Mock<IRepository<Subscription>>();
            repository.Setup(r => r.GetAll()).Returns(new List<Subscription>
            {
                new Subscription
                {
                  Id = "1",
                  EventId = "1",
                  OrganizerId = "1",
                },

               new Subscription
                {
                  Id = "2",
                  EventId = "2",
                  OrganizerId = "2",
                },
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Subscription, SubscriptionDTO>();
            });

            var mapper = mapperConfig.CreateMapper();
            var service = new SubscriptionService(repository.Object, mapper);

            var actual = service.GetSubscriptions("1");
            var expected = 1;

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetSubscriptionByIdTest()
        {
            var expected = new Subscription { Id = "1" };

            var repository = new Mock<IRepository<Subscription>>();
            repository.Setup(r => r.GetById(expected.Id)).Returns(new Subscription { Id = "1", EventId = "1" });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Subscription, SubscriptionDTO>();
            });

            var mapper = mapperConfig.CreateMapper();
            var service = new SubscriptionService(repository.Object, mapper);

            var actual = service.GetById("1");

            Assert.Equal(actual.Id, expected.Id);
        }
    }
}

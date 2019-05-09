using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Data.Interfaces;
using DataAccess.Data.Repositories;
using DataAccess.Entities;
using Eventor.Data;
using Eventor.Data.Entities;
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
    public class SubscriptionServiceTests
    {
        public IMapper Mapper { get; set; }

        public SubscriptionServiceTests()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Subscription, SubscriptionDTO>();
                cfg.CreateMap<SubscriptionDTO, Subscription>();
            })
           .CreateMapper();
        }

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
            var expected = new Event { Id = "1", Date = new DateTime(2020, 10, 10) };

            var actual = repository.GetById("1");
            var actualForNull = repository.GetById("1000");
            

            Assert.Equal(actual.Id, expected.Id);
            Assert.Null(actualForNull);
        }

        [Fact]
        public void AddTest()
        {
            var repository = new Mock<IRepository<Subscription>>();           
            var service = new SubscriptionService(repository.Object, Mapper);

            var expected = new SubscriptionDTO
            {
                Id = "1",
                EventId = "1",
                UserId = "1"
            };

            service.Add(expected);

            repository.Verify(r => r.Add(It.IsAny<Subscription>()), Times.Once);
        }

        [Fact]
        public void GetSubscriptionsTest()
        {
            var repository = SetupRepository();           
          
            var service = new SubscriptionService(repository.Object, Mapper);
            var expected = 2;

            var actual = service.GetSubscriptions("1");
           
            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetSubscriptionByIdTest()
        {
            var expected = new SubscriptionDTO { Id = "1", EventId = "1" };

            var repository = new Mock<IRepository<Subscription>>();
            repository.Setup(r => r.GetById(expected.Id)).Returns(new Subscription { Id = "1", EventId = "1"});
        
            var service = new SubscriptionService(repository.Object, Mapper);

            var actual = service.GetById("1");

            Assert.Equal(actual.Id, expected.Id);
            Assert.Equal(actual.EventId, expected.EventId);
        }

        [Fact]
        public void IsSubscribedTest()
        {          
            var repository = SetupRepository();
          
            var service = new SubscriptionService(repository.Object, Mapper);

            var actualForSubscribed = service.IsSubscribed(userId: "1", eventId: "1");
            var actualForUnsubscribed = service.IsSubscribed(userId: "1", eventId: "3");

            Assert.True(actualForSubscribed);
            Assert.False(actualForUnsubscribed);
        }

        [Fact]
        public void UnsubscribeAllTest()
        {
            var repository = SetupRepository();
         
            var service = new SubscriptionService(repository.Object, Mapper);
            service.UnsubscribeAll("2");

            repository.Verify(r => r.GetAll(), Times.Once);
            repository.Verify(r => r.DeleteById(It.IsAny<string>()), Times.Exactly(2));
            repository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void GetSubscriptionsForUserTest()
        {
            var repository = SetupRepository();
          
            var service = new SubscriptionService(repository.Object, Mapper);
            var expected = 2;

            var actual = service.GetSubscriptionsForUser("1");

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetNumberOfSubscribersTest()
        {
            var repository = SetupRepository();
  
            var service = new SubscriptionService(repository.Object, Mapper);
            var expected = 2;

            var actual = service.GetNumberOfSubscribers("2");

            Assert.Equal(actual, expected);
        }

        public Mock<IRepository<Subscription>> SetupRepository()
        {            
            var repository = new Mock<IRepository<Subscription>>();
            repository.Setup(r => r.GetAll()).Returns(new List<Subscription>
            {
                new Subscription
                {
                  Id = "1",
                  UserId = "1",
                  EventId = "1"
                },

               new Subscription
                {
                  Id = "2",
                  UserId = "1",
                  EventId = "2"
                },

               new Subscription
                {
                  Id = "3",
                  UserId = "2",
                  EventId = "2"
                },
            });

            return repository;
        }
    }
}

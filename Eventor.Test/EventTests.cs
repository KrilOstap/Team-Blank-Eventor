using AutoMapper;
using DataAccess.Data.Entities;
using DataAccess.Data.Interfaces;
using DataAccess.Data.Repositories;
using Eventor.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Eventor.Test
{
    public class EventTests
    {   
        [Fact]
        public void GetAllTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestEvent").Options;

            var context = new ApplicationDbContext(options);
            context.Events.Add(new Event { Id = "1", Date = new DateTime(2020, 10, 10) });
            context.Events.Add(new Event { Id = "2", Date = new DateTime(2018, 10, 10) });
            context.SaveChanges();

            var repository = new EventRepository(context);
            var actual = repository.GetAll().ToArray();
            var expected = 2;
            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetEventsTest()
        {
            var repository = new Mock<IRepository<Event>>();
            repository.Setup(r => r.GetAll()).Returns(new List<Event>
            {
                new Event
                {
                  Id = "1",
                  Title = "event 1"
                },

                new Event
                {
                  Id = "2",
                  Title = "event 2"
                },
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
            });

            var mapper = mapperConfig.CreateMapper();
            var service = new EventService(repository.Object, mapper);

            var actual = service.GetEvents();
            var expected = 2;

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetByIdTest()
        {
            var expected = new Event { Id = "1", Title = "event 1" };

            var repository = new Mock<IRepository<Event>>();
            repository.Setup(r => r.GetById(expected.Id)).Returns(new Event { Id = "1", Title = "event 1" });
           
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
            });

            var mapper = mapperConfig.CreateMapper();
            var service = new EventService(repository.Object, mapper);

            var actual = service.GetById("1");           

            Assert.Equal(actual.Id, expected.Id);
        }
    }
}

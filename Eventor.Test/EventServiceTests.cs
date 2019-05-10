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
        public IMapper Mapper { get; set; }

        public EventTests()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
                cfg.CreateMap<EventDTO, Event>();
            })
           .CreateMapper();
        }

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
            var expected = 2;

            var actual = repository.GetAll().ToArray();    
            
            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetFutureEventsTest()
        {
            var repository = new Mock<IRepository<Event>>();
            repository.Setup(r => r.GetAll()).Returns(new List<Event>
            {
                new Event
                {
                  Id = "1",
                  Title = "event 1",
                  Date  = DateTime.Now.AddYears(1)
                },

                new Event
                {
                  Id = "2",
                  Title = "event 2",
                  Date  = DateTime.Now.AddYears(1)
                },

                 new Event
                 {
                   Id = "2",
                   Title = "event 2",
                   Date  = DateTime.Now.AddYears(-1)
                 },
            });
         
            var service = new EventService(repository.Object, Mapper);
            var expected = 2;

            var actual = service.GetFutureEvents();            

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetAllEventsTest()
        {
            var repository = new Mock<IRepository<Event>>();
            repository.Setup(r => r.GetAll()).Returns(new List<Event>
            {
                new Event
                {
                  Id = "1",
                  Title = "event 1",
                  Date  = DateTime.Now.AddYears(1)
                },

                new Event
                {
                  Id = "2",
                  Title = "event 2",
                  Date  = DateTime.Now.AddYears(1)
                },

                 new Event
                 {
                   Id = "2",
                   Title = "event 2",
                   Date  = DateTime.Now.AddYears(-1)
                 },
            });

            var service = new EventService(repository.Object, Mapper);
            var expected = 3;

            var actual = service.GetAllEvents();

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetAllEventsForOrganizerTest()
        {
            var repository = new Mock<IRepository<Event>>();
            repository.Setup(r => r.GetAll()).Returns(new List<Event>
            {
                new Event
                {
                  Id = "1",
                  OrganizerId = "1",
                  Title = "event 1",
                  Date  = DateTime.Now.AddYears(1)
                },

                new Event
                {
                  Id = "2",
                  OrganizerId = "2",
                  Title = "event 2",
                  Date  = DateTime.Now.AddYears(1)
                },

                 new Event
                 {
                   Id = "2",
                   OrganizerId = "1",
                   Title = "event 2",
                   Date  = DateTime.Now.AddYears(-1)
                 },
            });

            var service = new EventService(repository.Object, Mapper);
            var expected = 2;

            var actual = service.GetAllEvents("1");

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void GetByIdTest()
        {
            var expected = new Event { Id = "1", Title = "event 1" };

            var repository = new Mock<IRepository<Event>>();
            repository.Setup(r => r.GetById(expected.Id)).Returns(new Event { Id = "1", Title = "event 1" });
                     
            var service = new EventService(repository.Object, Mapper);

            var actual = service.GetById("1");           

            Assert.Equal(actual.Id, expected.Id);
        }

        [Fact]
        public void AddTest()
        {
            var repository = new Mock<IRepository<Event>>();
            var service = new EventService(repository.Object, Mapper);

            var expected = new EventDTO
            {
                Id = "1",
                Title = "event 1"
            };

            service.Add(expected);

            repository.Verify(r => r.Add(It.IsAny<Event>()), Times.Once);
            repository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void DeleteTest()
        {
            var repository = new Mock<IRepository<Event>>();
            var service = new EventService(repository.Object, Mapper);

            var expected = new EventDTO
            {
                Id = "1",
                Title = "event 1"
            };

            service.Delete(expected.Id);
          
            repository.Verify(r => r.DeleteById(It.IsAny<string>()), Times.Once);
            repository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void UpdateTest()
        {
            var repository = new Mock<IRepository<Event>>();
            var service = new EventService(repository.Object, Mapper);

            var expected = new EventDTO
            {
                Id = "1",
                Title = "event 1"
            };

            service.Update(expected);

            repository.Verify(r => r.Update(It.IsAny<Event>()), Times.Once);
            repository.Verify(r => r.Save(), Times.Once);
        }
    }
}

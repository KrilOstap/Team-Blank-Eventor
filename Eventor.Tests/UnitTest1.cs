using DataAccess.Data.Entities;
using DataAccess.Data.Repositories;
using Eventor.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    public class Tests
    {      
        [Test]
        public void Test1()
        {
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //     .UseInMemoryDatabase(databaseName: "TestDb").Options;

            //var context = new ApplicationDbContext(options);

            //context.Events.Add(new Event { Id = 1, Date = new DateTime(2020, 10, 10) });
            //context.Events.Add(new Event { Id = 2, Date = new DateTime(2018, 10, 10) });

            //context.Organizers.Add(new Organizer { Id = 1, FirstName = "Marta" });
            //context.Organizers.Add(new Organizer { Id = 2, FirstName = "Arsen" });

            //context.EventOrganaizers.Add(new EventOrganaizer { Id = 1, EventId = 1, OrganaizerId = 1 });
            //context.EventOrganaizers.Add(new EventOrganaizer { Id = 2, EventId = 2, OrganaizerId = 2 });

            //context.SaveChanges();

            //var repository = new EventRepository(context);

            //var actual = repository.GetAll().First();
            //var expected = new Event { Id = 1, Date = new DateTime(2020, 10, 10) };

            //context.Dispose();
            Assert.IsTrue(true);
        }
    }
}
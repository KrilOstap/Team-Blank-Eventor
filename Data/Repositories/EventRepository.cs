using DataAccess.Data.Entities;
using DataAccess.Data.Interfaces;
using Eventor.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Data.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        private readonly ApplicationDbContext db;
        private bool disposed;

        public EventRepository(ApplicationDbContext db)
        {
            this.db = db;
            disposed = false;
        }

        public void Add(Event item)
        {
            db.Events.Add(item);
        }

        public void DeleteById(string id)
        {
            var entityToDelete = db.Events.Find(id);
            if (entityToDelete != null)
            {
                db.Remove(entityToDelete);
            }
        }

        public IEnumerable<Event> GetAll()
        {
            return db.Events.AsNoTracking().Include(e => e.Organizer)
                .Where(e => e.Date > DateTime.Now).OrderBy(e => e.Date);
        }
     
        public Event GetById(string id)
        {
            return db.Events.AsNoTracking()
                .Include(e => e.Organizer)                
                .FirstOrDefault(item => item.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Event item)
        {
            var @event = db.Events.Find(item.Id);
            @event.Title = item.Title;
            @event.Date = item.Date;
            @event.City = item.City;
            @event.Address = item.Address;
            @event.Number = item.Number;
            @event.Description = item.Description;
            db.Events.Update(@event);           
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

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
    public class OrganizerRepository : IRepository<Organizer>
    {
        private readonly ApplicationDbContext db;
        private bool disposed;

        public OrganizerRepository(ApplicationDbContext db)
        {
            this.db = db;
            disposed = false;
        }

        public void Add(Organizer item)
        {
            db.Organizers.Add(item);
        }

        public void DeleteById(int id)
        {
            var entityToDelete = db.Users.Find(id);
            if (entityToDelete != null)
            {
                db.Remove(entityToDelete);
            }
        }

        public IEnumerable<Organizer> GetAll()
        {
            return db.Organizers.AsNoTracking()
                .Include(item => item.EventOrganaizers)
                .ThenInclude(item => item.Event);
        }

        public Organizer GetById(int id)
        {
            return db.Organizers.AsNoTracking().FirstOrDefault(item => item.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(int id, Organizer item)
        {
            db.Organizers.Update(item);
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

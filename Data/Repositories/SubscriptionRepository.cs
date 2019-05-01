using DataAccess.Data.Interfaces;
using DataAccess.Entities;
using Eventor.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly ApplicationDbContext db;
        private bool disposed;

        public SubscriptionRepository(ApplicationDbContext db)
        {
            this.db = db;
            disposed = false;
        }

        public void Add(Subscription item)
        {
            db.Subscriptions.Add(item);
        }

        public void DeleteById(string id)
        {
            var entityToDelete = db.Subscriptions.Find(id);
            if (entityToDelete != null)
            {
                db.Remove(entityToDelete);
            }
        }

        public IEnumerable<Subscription> GetAll()
        {
            return db.Subscriptions.AsNoTracking()
                .Include(e => e.Event)
                .Include(o => o.User);                
        }

        public Subscription GetById(string id)
        {
            return db.Subscriptions.AsNoTracking()
                .Include(e => e.Event)
                .Include(o => o.User)
                .FirstOrDefault(item => item.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Subscription item)
        {
            db.Subscriptions.Update(item);
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

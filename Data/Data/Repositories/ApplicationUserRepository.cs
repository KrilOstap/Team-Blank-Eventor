using DataAccess.Data.Interfaces;
using Eventor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventor.Data.Repositories
{
    public class ApplicationUserRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext db;
        private bool disposed;

        public ApplicationUserRepository(ApplicationDbContext db)
        {
            this.db = db;
            disposed = false;
        }

        public void Add(ApplicationUser item)
        {
            db.Users.Add(item);
        }

        public void DeleteById(int id)
        {
            var entityToDelete = db.Users.Find(id);
            if (entityToDelete != null)
            {
                db.Remove(entityToDelete);
            }         
        }
       
        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users.AsNoTracking();                
        }

        public IEnumerable<string> GetAllEmails()
        {            
            return db.Users.Select(item => item.Email);       
        }

        public ApplicationUser GetById(int id)
        {
            return db.Users.AsNoTracking().FirstOrDefault(item => item.Id == id.ToString());
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(int id, ApplicationUser item)
        {
            db.Users.Update(item);
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

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(string id);
        void Add(TEntity item);
        void DeleteById(string id);
        void Update(TEntity item);
        void Save();
    }
}

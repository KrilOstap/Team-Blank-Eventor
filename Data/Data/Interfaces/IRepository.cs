using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Add(TEntity item);
        void DeleteById(int id);
        void Update(int id, TEntity item);
        void Save();
    }
}

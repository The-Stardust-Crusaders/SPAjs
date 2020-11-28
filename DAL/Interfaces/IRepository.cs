using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Create(T entry);
        void Update(T entry);
        void Delete(string id);
    }
}

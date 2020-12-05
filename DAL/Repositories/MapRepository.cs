using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class MapRepository : IRepository<CustomMap>
    {
        private readonly ApplicationContext context;

        public MapRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(CustomMap entry)
        {
            context.Maps.Add(entry);
        }

        public void Delete(string id)
        {
            CustomMap entry = GetById(id);
            if (entry != null)
            {
                context.Remove(entry);
            }
        }

        public IEnumerable<CustomMap> Get(Func<CustomMap, bool> predicate)
        {
            return context.Maps.Include(map => map.Request).Where(predicate).ToList();
        }

        public IEnumerable<CustomMap> GetAll()
        {
            return context.Maps.Include(map => map.Request).ToList();
        }

        public CustomMap GetById(string id)
        {
            return context.Maps.Find(id);
        }

        public void Update(CustomMap entry)
        {
            context.Entry(entry).State = EntityState.Modified;
        }
    }
}

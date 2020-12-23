using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class RepostRepository : IRepository<Repost>
    {
        private readonly ApplicationContext context;

        public RepostRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Repost entry)
        {
            context.Reposts.Add(entry);
        }

        public void Delete(string id)
        {
            Repost entry = GetById(id);
            if (entry != null)
            {
                context.Reposts.Remove(entry);
            }
        }

        public IEnumerable<Repost> Get(Func<Repost, bool> predicate)
        {
            return context.Reposts.Include(rep => rep.Request).Include(rep => rep.UserProfile).Where(predicate).ToList();
        }

        public IEnumerable<Repost> GetAll()
        {
            return context.Reposts.Include(rep => rep.Request).Include(rep => rep.UserProfile).ToList();
        }

        public Repost GetById(string id)
        {
            return context.Reposts.Find(id);
        }

        public void Update(Repost entry)
        {
            context.Entry(entry).State = EntityState.Modified;
        }
    }
}

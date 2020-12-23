using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class RequestRepository : IRepository<Request>
    {
        private readonly ApplicationContext context;

        public RequestRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Request entry)
        {
            context.Requests.Add(entry);
        }

        public void Delete(string id)
        {
            Request entry = GetById(id);
            if (entry != null)
            {
                context.Requests.Remove(entry);
            }
        }

        public IEnumerable<Request> Get(Func<Request, bool> predicate)
        {
            return context.Requests.Include(req => req.Maps).Include(req => req.Reposts).Include(req => req.Sender).Where(predicate).ToList();
        }

        public IEnumerable<Request> GetAll()
        {
            return context.Requests.Include(req => req.Maps).Include(req => req.Reposts).Include(req => req.Sender).ToList();
        }

        public Request GetById(string id)
        {
            return context.Requests.Find(id);
        }

        public void Update(Request entry)
        {
            context.Entry(entry).State = EntityState.Modified;
        }
    }
}

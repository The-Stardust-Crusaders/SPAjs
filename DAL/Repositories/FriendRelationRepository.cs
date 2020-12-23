using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class FriendRelationRepository : IRepository<FriendRelation>
    {
        private readonly ApplicationContext context;

        public FriendRelationRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(FriendRelation entry)
        {
            context.FriendRelations.Add(entry);
        }

        public void Delete(string id)
        {
            FriendRelation entry = GetById(id);
            if(entry != null)
            {
                context.FriendRelations.Remove(entry);
            }
        }

        public IEnumerable<FriendRelation> Get(Func<FriendRelation, bool> predicate)
        {
            return context.FriendRelations.Include(fr => fr.Initiator).Where(predicate).ToList();
        }

        public IEnumerable<FriendRelation> GetAll()
        {
            return context.FriendRelations.Include(fr => fr.Initiator).ToList();
        }

        public FriendRelation GetById(string id)
        {
            return context.FriendRelations.Find(id);
        }

        public void Update(FriendRelation entry)
        {
            context.Entry(entry).State = EntityState.Modified;
        }
    }
}

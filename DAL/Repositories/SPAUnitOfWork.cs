using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Repositories
{
    public class SPAUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;
        private FriendRelationRepository friendRelationRepository;
        private MapRepository mapRepository;
        private RepostRepository repostRepository;
        private RequestRepository requestRepository;
        private bool disposed;

        public SPAUnitOfWork(DbContextOptions<ApplicationContext> options)
        {
            context = new ApplicationContext(options);
            disposed = false;
        }

        public IRepository<FriendRelation> FriendRelations
        {
            get
            {
                if (friendRelationRepository == null)
                {
                    friendRelationRepository = new FriendRelationRepository(context);
                }
                return friendRelationRepository;
            }
        }

        public IRepository<CustomMap> Maps
        {
            get
            {
                if (mapRepository == null)
                {
                    mapRepository = new MapRepository(context);
                }
                return mapRepository;
            }
        }

        public IRepository<Repost> Reposts
        {
            get
            {
                if (repostRepository == null)
                {
                    repostRepository = new RepostRepository(context);
                }
                return repostRepository;
            }
        }

        public IRepository<Request> Requests
        {
            get
            {
                if (requestRepository == null)
                {
                    requestRepository = new RequestRepository(context);
                }
                return requestRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}

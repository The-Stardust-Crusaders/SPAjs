using DAL.Entities;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Request> Requests { get; }
        IRepository<CustomMap> Maps { get; }
        IRepository<Repost> Reposts { get; }
        IRepository<FriendRelation> FriendRelations { get; }
        void SaveChanges();
    }
}

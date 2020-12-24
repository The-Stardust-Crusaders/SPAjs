using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRepostService
    {
        Task CreateRepost(RepostDTO data);

        void DeleteRepost(string id);

        IEnumerable<Repost> GetRepostsByIdUser(string UserId);
    }
}
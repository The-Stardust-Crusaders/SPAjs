using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks; 

namespace BLL.Interfaces
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendRelation>> GetFriendRelations(ClaimsPrincipal user);
        Task AddFriend(FriendDTO friendDTO, ClaimsPrincipal activeUser);
        Task CancelFriendRelation(FriendDTO friendDTO, ClaimsPrincipal activeUser);
    }
}

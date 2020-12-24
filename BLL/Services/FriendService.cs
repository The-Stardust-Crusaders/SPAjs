using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FriendService : IFriendService
    {
        private readonly UserManager<UserProfile> userManager;
        private readonly IUnitOfWork unitOfWork;

        public FriendService(UserManager<UserProfile> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FriendRelation>> GetFriendRelations(ClaimsPrincipal user)
        {
            UserProfile activeProfile = await userManager.GetUserAsync(user);

            if (activeProfile == null)
            {
                throw new ApplicationException("Active User not found");
            }

            return unitOfWork.FriendRelations.Get(fr => fr.InitiatorId.Equals(activeProfile.Id));
        }

        public async Task AddFriend(FriendDTO friendDTO, ClaimsPrincipal activeUser)
        {
            UserProfile activeProfile = await userManager.GetUserAsync(activeUser);

            if (activeProfile == null) {
                throw new ApplicationException("Active User not found");
            }

            var rel = new FriendRelation { InitiatorId = activeProfile.Id, FriendId = friendDTO.FriendId };
            unitOfWork.FriendRelations.Create(rel);
            unitOfWork.SaveChanges();
        }

        public async Task CancelFriendRelation(FriendDTO friendDTO, ClaimsPrincipal activeUser)
        {
            UserProfile activeProfile = await userManager.GetUserAsync(activeUser);

            if (activeProfile == null)
            {
                throw new ApplicationException("Active User not found");
            }

            var rel = unitOfWork.FriendRelations.Get(fr => fr.FriendId.Equals(friendDTO.FriendId) && fr.InitiatorId.Equals(activeProfile.Id)).First();
            
            if (rel == null)
            {
                throw new ApplicationException("no relation with specified user");
            }

            unitOfWork.FriendRelations.Delete(rel.Id);
            unitOfWork.SaveChanges();
        }
    }
}

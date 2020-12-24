using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RepostService : IRepostService
    {
        private readonly IUnitOfWork SPAUoF;

        private readonly UserManager<UserProfile> userManager;

        public RepostService(IUnitOfWork SPAUnitOfWork, UserManager<UserProfile> userManager)
        {
            this.SPAUoF = SPAUnitOfWork;
            this.userManager = userManager;
        }

        public async Task CreateRepost(RepostDTO data)
        {
            //var user = userManager.Users.SingleOrDefault(u => u.Id == data.UserProfileId);
            var user = await userManager.FindByIdAsync(data.UserProfileId);

            Request request = SPAUoF.Requests.GetById(data.RequestId);


            Repost repost = new Repost
            {
                UserProfile = user,
                Request = request
            };

            SPAUoF.Reposts.Create(repost);
            SPAUoF.SaveChanges();

        }

        public void DeleteRepost(string id)
        {

            SPAUoF.Reposts.Delete(id);
            SPAUoF.SaveChanges();

        }

        public IEnumerable<Repost> GetRepostsByIdUser(string UserId)
        {

            return SPAUoF.Reposts.Get(rp => rp.UserProfileId == UserId);

        }

    }
}
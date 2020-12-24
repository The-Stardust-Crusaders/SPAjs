using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public async Task<object> CreateRepost(RepostDTO data)
        {
            //var user = userManager.Users.SingleOrDefault(u => u.Id == data.UserProfileId);
            var user = await userManager.Users.FindByIdAsync(data.UserProfileId);

            Request request = await SPAUoF.Requests.GetById(data.RequestId);


            Repost repost = new Repost
            {
                UserProfile = user,
                Request = request
            };

            SPAUoF.Reposts.Create(repost);
            SPAUoF.SaveChanges();

        }

        public async Task<object> DeleteRepost(string? id)
        {

            SPAUoF.Reposts.Delete(id);
            SPAUoF.SaveShanges();

        }

        public async IEnumerable<Repost> GetRepostsByIdUser(string? UserId)
        {

            return SPAUoF.Reposts.Get(rp => rp.UserProfileId == UserId);

        }

    }
}
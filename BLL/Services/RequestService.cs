using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<UserProfile> userManager;

        public RequestService(IUnitOfWork unitOfWork, UserManager<UserProfile> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Request>> GetRequests(ClaimsPrincipal user)
        {
            var userProfile = await userManager.GetUserAsync(user);
            if (userProfile == null)
            {
                throw new ApplicationException("user not found");
            }
            return unitOfWork.Requests.Get(rq => rq.SenderId == userProfile.Id); 
        }

        public async Task<Request> GetRequestById(string requestId, ClaimsPrincipal user)
        {
            var userProfile = await userManager.GetUserAsync(user);
            if (userProfile == null)
            {
                throw new ApplicationException("user not found");
            }
            var request = unitOfWork.Requests.GetById(requestId);
            return request.SenderId == userProfile.Id ? request : null;
        }

        public async Task CreateRequest(RequestDTO requestDTO, ClaimsPrincipal user)
        {
            var userProfile = await userManager.GetUserAsync(user);
            if (userProfile == null)
            {
                throw new ApplicationException("user not found");
            }
            var request = new Request
            {
                LandsatName = requestDTO.LandsatName,
                CloudCover = requestDTO.CloudCover,
                DateTime = requestDTO.DateTime,
                Status = requestDTO.Status,
                Description = requestDTO.Description,
                SenderId = userProfile.Id
            };
            unitOfWork.Requests.Create(request);
            unitOfWork.SaveChanges();
        }

        public async Task UpdateRequest(string requestId, RequestDTO requestDTO, ClaimsPrincipal user)
        {
            var userProfile = await userManager.GetUserAsync(user);
            if (userProfile == null)
            {
                throw new ApplicationException("user not found");
            }
            var request = unitOfWork.Requests.GetById(requestId);
            if (request.SenderId == userProfile.Id)
            {
                if (!string.IsNullOrEmpty(requestDTO.LandsatName.Trim()))
                {
                    request.LandsatName = requestDTO.LandsatName;
                }
                request.CloudCover = requestDTO.CloudCover;
                request.DateTime = requestDTO.DateTime;
                request.Status = requestDTO.Status;
                if (!string.IsNullOrEmpty(requestDTO.Description.Trim()))
                {
                    request.Description = requestDTO.Description;
                }
                unitOfWork.Requests.Update(request);
                unitOfWork.SaveChanges();
            }
        }

        public async Task DeleteRequest(string requestId, ClaimsPrincipal user)
        {
            var userProfile = await userManager.GetUserAsync(user);
            if (userProfile == null)
            {
                throw new ApplicationException("user not found");
            }
            var request = unitOfWork.Requests.GetById(requestId);
            if (request.SenderId == userProfile.Id)
            {
                unitOfWork.Requests.Delete(requestId);
                unitOfWork.SaveChanges();
            }
        }
    }
}

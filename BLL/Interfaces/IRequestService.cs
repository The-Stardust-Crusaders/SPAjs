using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> GetRequests(ClaimsPrincipal user);
        Task<Request> GetRequestById(string requestId, ClaimsPrincipal user);
        Task CreateRequest(RequestDTO requestDTO, ClaimsPrincipal user);
        Task UpdateRequest(string requestId, RequestDTO requestDTO, ClaimsPrincipal user);
        Task DeleteRequest(string requestId, ClaimsPrincipal user);
    }
}

using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRepostService
    {
        Task<object> CreateRepost(RepostDTO data);

        Task<object> DeleteRepost(string? id);

        IEnumerable<RepostDTO> GetRepostsByIdUser(string? UserId);
    }
}
using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserProfileService
    {
        Task<object> Register(RegisterDTO data);
        Task<object> Login(LoginDTO data);
    }
}

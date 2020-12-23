using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRegisterService
    {
        Task<object> Register(RegisterDTO data);
    }
}

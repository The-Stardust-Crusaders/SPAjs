using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SPAjs.Models;
using System.Threading.Tasks;

namespace SPAjs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserProfileController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public UserProfileController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            IActionResult response;

            try
            {
                var dto = new RegisterDTO
                {
                    UserName = model.UserName,
                    UserEmail = model.UserEmail,
                    UserPass = model.UserPass,
                    ConfirmPass = model.ConfirmPass
                };
                var accessToken = await registerService.Register(dto);
                response = Ok(accessToken);
            } 
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                response = Conflict(ModelState);
            }

            return response;
        }
    }
}

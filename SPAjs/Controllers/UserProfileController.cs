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
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
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
                var accessToken = await userProfileService.Register(dto);
                response = Ok(accessToken);
            } 
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                response = Conflict(ModelState);
            }

            return response;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            IActionResult response;

            try
            {
                var dto = new LoginDTO
                {
                    Email = model.UserEmail,
                    Password = model.UserPass
                };
                var accessToken = await userProfileService.Login(dto);
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

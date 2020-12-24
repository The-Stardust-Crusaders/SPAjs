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
    public class RepostController : ControllerBase
    {
        private readonly IRepostService repostSevice;

        public RepostController(IRepostService repostService)
        {
            this.repostSevice = repostSevice;
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetUserReposts")]
        public async Task<IActionResult> GetUserReposts(string id)
        {
            IActionResult response;

            try
            {
                var reposts = await repostSevice.GetRepostsByIdUser(id);
                response = Ok(reposts);
            } 
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                response = Conflict(ModelState);
            }

            return response;

        }

        [Authorize]
        [HttpPost("AddRepost")]
        public async Task<IActionResult> AddRepost(AddRepostViewModel model)
        {
            IActionResult response;

            try
            {
                var dto = new RepostDTO
                {
                    UserProfileId = model.UserProfileId,
                    RequestId = model.RequestId
                };
                var accessToken = await repostSevice.CreateRepost(dto);
                response = Ok(accessToken);
            } 
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                response = Conflict(ModelState);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("service error", ex.Message);
                response = Conflict(ModelState);
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{repostId}", Name = "DeleteRepost")]
        public async Task<IActionResult> DeleteRepost(string repostId)
        {
            IActionResult response;

            try
            {
                await friendService.DeleteRepost(repostId);
                response = Ok();
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                response = Conflict(ModelState);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("service error", ex.Message);
                response = Conflict(ModelState);
            }

            return response;
        }




    }
}

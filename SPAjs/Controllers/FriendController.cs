using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPAjs.Models;
using System;
using System.Threading.Tasks;

namespace SPAjs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService friendService;

        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFriendRelations()
        {
            IActionResult response;

            try
            {
                var entries = await friendService.GetFriendRelations(User);
                response = Ok(entries);
            }
            catch (ApplicationException ex2)
            {
                ModelState.AddModelError("service error", ex2.Message);
                response = Conflict(ModelState);
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFriend(FriendViewModel model)
        {
            IActionResult response;

            try
            {
                var dto = new FriendDTO
                {
                    FriendId = model.FriendId
                };
                await friendService.AddFriend(dto, User);
                response = Ok();
            }
            catch(ValidationException exception)
            {
                ModelState.AddModelError(exception.Property, exception.Message);
                response = Conflict(ModelState);
            }
            catch(ApplicationException ex2)
            {
                ModelState.AddModelError("service error", ex2.Message);
                response = Conflict(ModelState);
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{friendId}")]
        public async Task<IActionResult> CancelFriendRelation(string friendId)
        {
            IActionResult response;

            try
            {
                var dto = new FriendDTO
                {
                    FriendId = friendId
                };
                await friendService.CancelFriendRelation(dto, User);
                response = Ok();
            }
            catch (ValidationException exception)
            {
                ModelState.AddModelError(exception.Property, exception.Message);
                response = Conflict(ModelState);
            }
            catch (ApplicationException ex2)
            {
                ModelState.AddModelError("service error", ex2.Message);
                response = Conflict(ModelState);
            }

            return response;
        }
    }
}

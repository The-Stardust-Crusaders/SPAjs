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
    public class RequestController : ControllerBase
    {
        private readonly IRequestService requestService;

        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            IActionResult response;

            try
            {
                var query = await requestService.GetRequests(User);
                response = Ok(query);
            }
            catch(ApplicationException appException)
            {
                response = BadRequest(appException.Message);
            }

            return response;
        }

        [Authorize]
        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetRequest(string requestId)
        {
            IActionResult response;

            try
            {
                var query = await requestService.GetRequestById(requestId, User);
                response = Ok(query);
            }
            catch (ApplicationException appException)
            {
                response = BadRequest(appException.Message);
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestViewModel model)
        {
            IActionResult response;

            try
            {
                var dto = new RequestDTO
                {
                    LandsatName = model.LandsatName,
                    CloudCover = model.CloudCover,
                    DateTime = model.DateTime,
                    Status = model.Status,
                    Description = model.Description
                };
                await requestService.CreateRequest(dto, User);
                response = Ok();
            }
            catch (ValidationException validException)
            {
                ModelState.AddModelError(validException.Property, validException.Message);
                response = Conflict(ModelState);
            }
            catch (ApplicationException appException)
            {
                response = BadRequest(appException.Message);
            }

            return response;
        }

        [Authorize]
        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateRequest(RequestViewModel model, string requestId)
        {
            IActionResult response;

            try
            {
                var dto = new RequestDTO
                {
                    LandsatName = model.LandsatName,
                    CloudCover = model.CloudCover,
                    DateTime = model.DateTime,
                    Status = model.Status,
                    Description = model.Description
                };
                await requestService.UpdateRequest(requestId, dto, User);
                response = Ok();
            }
            catch (ValidationException validException)
            {
                ModelState.AddModelError(validException.Property, validException.Message);
                response = Conflict(ModelState);
            }
            catch (ApplicationException appException)
            {
                response = BadRequest(appException.Message);
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(string requestId)
        {
            IActionResult response;

            try
            {
                await requestService.DeleteRequest(requestId, User);
                response = Ok();
            }
            catch (ApplicationException appException)
            {
                response = BadRequest(appException.Message);
            }

            return response;
        }
    }
}

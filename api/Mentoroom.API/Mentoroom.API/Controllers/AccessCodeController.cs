using MediatR;
using Mentoroom.APPLICATION.Managements.Auth.AccessCodes.DeactivateAccessCode;
using Mentoroom.APPLICATION.Managements.Auth.AccessCodes.GenerateAccessCode;
using Mentoroom.APPLICATION.Managements.Auth.AccessCodes.GetAccessCodes;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccessCodeController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAccessCode()
        {
            var accessCodes = await mediator.Send(new GetAccessCodesCommand());
            return Ok(accessCodes);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> GenerateAccessCode(GenerateAccessCodeQuerry generateAccessCodeQuerry)
        {
            var accessCode = await mediator.Send(generateAccessCodeQuerry);
            return Ok(accessCode);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateAccessCode(string id)
        {
            var accessCode = await mediator.Send(new DeactivateAccessCode() { CodeID = id });
            return Ok(accessCode);
        }
    }
}

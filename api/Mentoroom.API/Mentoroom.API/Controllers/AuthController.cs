using MediatR;
using Mentoroom.APPLICATION.Managements.Auth.Login;
using Mentoroom.APPLICATION.Managements.Auth.Logout;
using Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToAdmin;
using Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToLecturer.PromoteWithAccessCode;
using Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToLecturrer;
using Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToStudent;
using Mentoroom.APPLICATION.Managements.Auth.RefreshToken;
using Mentoroom.APPLICATION.Managements.Auth.Register;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(Register request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(Login request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshToken request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await mediator.Send(new Logout() { ClaimsPrincipal = User });
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("promote-to-admin")]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var result = await mediator.Send(new PromoteToAdmin() { UserID = userId });
            return Ok(result);
        }

        [Authorize]
        [HttpPost("promote-to-student")]
        public async Task<IActionResult> PromoteToStudent(string userId, string studentIndex)
        {
            var result = await mediator.Send(new PromoteToStudent()
            {
                UserID = userId,
                StudentIndex = studentIndex
            });

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("promote-to-lecturer")]
        public async Task<IActionResult> PromoteToLecturerAsAdmin(string userId)
        {
            var result = await mediator.Send(new PromoteToLecturer() { UserID = userId });
            return Ok(result);
        }

        [Authorize]
        [HttpPost("promote-to-lecturer-with-access-code")]
        public async Task<IActionResult> PromoteToLecturer(string accessCode)
        {
            await mediator.Send(new PromoteToLecturerWithAccessCode() { UserID = User.Claims.First(x => x.Type.Equals("Id")).Value, AccessCode = accessCode });
            return Ok();
        }
    }
}

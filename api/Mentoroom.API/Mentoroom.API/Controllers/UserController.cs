using MediatR;
using Mentoroom.APPLICATION.Managements.Users.Queries.GetLecturers;
using Mentoroom.APPLICATION.Managements.Users.Queries.GetUsers;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await mediator.Send(new GetUsersQuerry());
            return Ok(users);
        }


        [HttpGet("Lecturers")]
        public async Task<IActionResult> GetAllLecturers()
        {
            var users = await mediator.Send(new GetLecturersQuerry());
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            //TODO
            return Ok();
        }
    }
}

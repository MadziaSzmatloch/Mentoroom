using MediatR;
using Mentoroom.APPLICATION.Managements.Lecturer.GetLecturers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLecturers()
        {
            var lecturers = await mediator.Send(new GetLecturesCommand());
            return Ok(lecturers);
        }
    }
}

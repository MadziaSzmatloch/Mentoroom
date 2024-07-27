using MediatR;
using Mentoroom.APPLICATION.Managements.Tags.Commands.AddSemester;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Semester.GetSemester;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Semester.GetSemesterByYear;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers.TagsControllers
{
    [ApiController]
    [Route("api/tags/[controller]")]
    public class SemesterController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddSemester(AddSemester addSemester)
        {
            var result = await mediator.Send(addSemester);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSemester()
        {
            var semesters = await mediator.Send(new GetSemester());
            return Ok(semesters);
        }

        [HttpGet("{yearId}")]
        public async Task<IActionResult> GetSemestersByYear(Guid yearId)
        {
            var years = await mediator.Send(new GetSemesterByYear() { YearId = yearId });
            return Ok(years);
        }
    }
}

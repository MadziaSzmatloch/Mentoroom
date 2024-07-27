using MediatR;
using Mentoroom.APPLICATION.Managements.Tags.Commands.AddMajor;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Major.GetMajor;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Major.GetMajorByDepartment;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers.TagsControllers
{
    [ApiController]
    [Route("api/tags/[controller]")]
    public class MajorController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddMajor(AddMajor addMajor)
        {
            var result = await mediator.Send(addMajor);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMajors()
        {
            var majors = await mediator.Send(new GetMajor());
            return Ok(majors);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetMajorsByDepartmentId(Guid departmentId)
        {
            var majors = await mediator.Send(new GetMajorByDepartment() { DepartmentId = departmentId });
            return Ok(majors);
        }
    }
}

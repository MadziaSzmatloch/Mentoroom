using MediatR;
using Mentoroom.APPLICATION.Managements.Tags.Commands.AddDepartment;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Department;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers.TagsControllers
{
    [ApiController]
    [Route("api/tags/[controller]")]

    public class DepartmentController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartment addDepartment)
        {
            var result = await mediator.Send(addDepartment);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await mediator.Send(new GetDepartment());
            return Ok(departments);
        }
    }
}

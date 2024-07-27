using MediatR;
using Mentoroom.APPLICATION.Managements.Tags.Commands.AddDegree;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Degree;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers.TagsControllers
{
    [ApiController]
    [Route("api/tags/[controller]")]
    public class DegreeController(IMediator mediator) : Controller
    {

        [HttpPost]
        public async Task<IActionResult> AddDegree(AddDegree addDegree)
        {
            var result = await mediator.Send(addDegree);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDegrees()
        {
            var degrees = await mediator.Send(new GetDegree());
            return Ok(degrees);
        }
    }
}

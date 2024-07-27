using MediatR;
using Mentoroom.APPLICATION.Managements.Tags.Commands.AddYear;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Year.GetYear;
using Mentoroom.APPLICATION.Managements.Tags.Queries.Year.GetYearByDegree;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers.TagsControllers
{
    [ApiController]
    [Route("api/tags/[controller]")]
    public class YearController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddYear(AddYear addYear)
        {
            var result = await mediator.Send(addYear);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYears()
        {
            var years = await mediator.Send(new GetYear());
            return Ok(years);
        }

        [HttpGet("{degreeId}")]
        public async Task<IActionResult> GetYearsByDegree(Guid degreeId)
        {
            var years = await mediator.Send(new GetYearByDegree() { DegreeId = degreeId });
            return Ok(years);
        }
    }
}

using MediatR;
using Mentoroom.APPLICATION.Managements.Course.Commands.AddCoAuthorToCourse;
using Mentoroom.APPLICATION.Managements.Course.Commands.AddCourse;
using Mentoroom.APPLICATION.Managements.Course.Commands.CoAuthor.DeleteCoAuthorFromCourse;
using Mentoroom.APPLICATION.Managements.Course.Commands.DeleteCourse;
using Mentoroom.APPLICATION.Managements.Course.Commands.UpdateCourse;
using Mentoroom.APPLICATION.Managements.Course.Queries.GetCourse;
using Mentoroom.APPLICATION.Managements.Course.Queries.GetCourseByTeacher;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [HttpGet("/hello")]
        public async Task<IActionResult> HelloWorld()
        {
            return Ok("Hello world");
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourse courseCommand)
        {
            var courseDto = await mediator.Send(courseCommand);

            return Ok(courseDto);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await mediator.Send(new GetCourse());
            return Ok(courses);
        }

        [HttpGet("{teacherId}")]
        //[Authorize]
        public async Task<IActionResult> GetCoursesByTeacherId(string teacherId)
        {
            var courses = await mediator.Send(new GetCourseByTeacher() { TeacherId = teacherId });
            return Ok(courses);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            await mediator.Send(new DeleteCourse() { CourseId = id });

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCourse(UpdateCourse updateCourse)
        {
            var courseDto = await mediator.Send(updateCourse);
            return Ok(courseDto);
        }

        [HttpPatch("/addCoAuthors")]
        public async Task<IActionResult> AddCoAuthorsToCourse(AddCoAuthorToCourse addCoAuthor)
        {
            var userModel = await mediator.Send(addCoAuthor);
            return Ok(userModel);
        }

        [HttpPatch("/deleteCoAuthors")]
        public async Task<IActionResult> DeleteCoAuthorsToCourse(DeleteCoAuthorFromCourse deleteCoAuthor)
        {
            await mediator.Send(deleteCoAuthor);
            return Ok();
        }
    }
}

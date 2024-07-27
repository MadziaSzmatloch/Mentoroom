using MediatR;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentAssignment.Queries.GetStudentAssignment;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAssignmentController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetStudentAssignment(string studentId, Guid assignmentId)
        {
            var studentAss = await mediator.Send(new GetStudentAssignment() { StudentId = studentId, AssignmnetId = assignmentId });
            return Ok(studentAss);
        }
    }
}

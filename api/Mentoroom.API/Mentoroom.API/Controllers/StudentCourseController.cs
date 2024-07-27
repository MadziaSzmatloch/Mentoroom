using MediatR;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.AddStudentCourse;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.ConfirmStudentCourse;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.DeleteStudentCourse;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.RemoveStudentCourse;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetCoursesFromStudent;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentCourse;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentsFromCourse;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentsToConfirm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddStudentCourse(AddStudentCourse addStudentCourse)
        {
            await mediator.Send(addStudentCourse);
            return Ok();
        }

        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteStudentCourse(string studentId, Guid courseId)
        {
            await mediator.Send(new DeleteStudentCourse()
            {
                StudentId = studentId,
                CourseId = courseId,
            });
            return Ok();
        }

        //get one student course
        [HttpGet("studentCourse/{courseId}")]
        [Authorize]
        public async Task<IActionResult> GetStudentCourse(Guid courseId)
        {
            var studentId = User.Claims.First(x => x.Type.Equals("Id")).Value;
            var courses = await mediator.Send(new GetStudentCourse() { CourseId = courseId, StudentId = studentId });
            return Ok(courses);
        }

        //get courses from student
        [HttpGet("students/{studentId}")]
        //[Authorize]
        public async Task<IActionResult> GetCoursesByStudentId(string studentId)
        {
            var courses = await mediator.Send(new GetCoursesFromStudent() { StudentId = studentId });
            return Ok(courses);
        }

        //get students from course
        [HttpGet("courses/{courseId}")]
        //[Authorize]
        public async Task<IActionResult> GetStudentByCourseId(Guid courseId)
        {
            var students = await mediator.Send(new GetStudentsFromCourse() { CourseId = courseId });
            return Ok(students);
        }

        //get students to confirm from course
        [HttpGet("unconfirmedStudents/{courseId}")]
        //[Authorize]
        public async Task<IActionResult> GetUnconfirmedStudentByCourseId(Guid courseId)
        {
            var students = await mediator.Send(new GetStudentsToConfirm() { CourseId = courseId });
            return Ok(students);
        }
        [HttpPatch("confirm")]
        public async Task<IActionResult> ConfirmStudentCourse(ConfirmStudentCourse confirmStudentCourse)
        {
            await mediator.Send(confirmStudentCourse);
            return Ok();
        }
        [HttpPatch("remove")]
        public async Task<IActionResult> RemoveStudentCourse(RemoveStudentCourse removeStudentCourse)
        {
            await mediator.Send(removeStudentCourse);
            return Ok();
        }

    }
}

using MediatR;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Commands.Delete;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Commands.Upload;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFileById;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFilesByAssignment;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFilesByCourse;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentFileController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadStudentFile uploadStudentFile)
        {
            var result = await mediator.Send(uploadStudentFile);
            return Ok(result);
        }

        [HttpGet]
        [Route("course/{courseId}")]
        public async Task<IActionResult> DownloadByCourse(Guid courseId)
        {
            var result = await mediator.Send(new DownloadStudentFilesByCourse() { CourseId = courseId });
            return File(result.Content, result.ContentType, result.Name);
            //return Ok();
        }

        [HttpGet]
        [Route("assignment/{assignmentId}")]
        public async Task<IActionResult> DownloadByAssignment(Guid assignmentId)
        {
            var result = await mediator.Send(new DownloadStudentFilesByAssignment() { AssignmentId = assignmentId });
            return File(result.Content, result.ContentType, result.Name);
            //return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentFile(Guid id)
        {
            var studentId = User.Claims.First(x => x.Type.Equals("Id")).Value;
            await mediator.Send(new DeleteStudentFile() { StudentId = studentId, AssignmentFileId = id });
            return Ok();
        }
        [HttpGet]
        [Route("{assignmentFileId}")]
        public async Task<IActionResult> DownloadStudentFileById(Guid assignmentFileId)
        {
            var studentId = User.Claims.First(x => x.Type.Equals("Id")).Value;
            var result = await mediator.Send(new DownloadStudentFileById() { StudentId = studentId, AssignmentFileId = assignmentFileId });
            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

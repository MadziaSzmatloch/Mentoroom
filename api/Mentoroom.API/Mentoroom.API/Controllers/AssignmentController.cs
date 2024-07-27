using MediatR;
using Mentoroom.APPLICATION.Managements.Assignment.Commands.AddAssignment;
using Mentoroom.APPLICATION.Managements.Assignment.Commands.DeleteAssignment;
using Mentoroom.APPLICATION.Managements.Assignment.Commands.UpdateAssignment;
using Mentoroom.APPLICATION.Managements.Assignment.Files.AddAssignmentFile;
using Mentoroom.APPLICATION.Managements.Assignment.Files.DeleteAssignmentFile;
using Mentoroom.APPLICATION.Managements.Assignment.Files.DownloadAssignmentFile;
using Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignmentById;
using Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignmentByStudentId;
using Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignments;
using Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.AddRequiredFile;
using Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.DeleteRequiredFile;
using Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.UpdateRequiredFile;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController(IMediator mediator) : Controller
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddAssignmemt(AddAssignment addAssignment)
        {
            var assignmentDto = await mediator.Send(addAssignment);
            return Ok(assignmentDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssignmemts()
        {
            var assignments = await mediator.Send(new GetAssignments());
            return Ok(assignments);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAssignmemtsByCourseId(Guid courseId)
        {
            var assignments = await mediator.Send(new GetAssignmentByCourseId() { CourseId = courseId });
            return Ok(assignments);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetAssignmemtsByStudentId(string studentId)
        {
            var assignments = await mediator.Send(new GetAssignmentByStudentId() { StudentId = studentId });
            return Ok(assignments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(Guid id)
        {
            await mediator.Send(new DeleteAssignment() { AssignmentId = id });
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAssignment(UpdateAssignment updateAssignment)
        {
            var assignment = await mediator.Send(updateAssignment);
            return Ok(assignment);
        }

        [HttpPost("file")]
        public async Task<IActionResult> UploadFile(AddAssignmentFile addAssignmentFile)
        {
            var assignment = await mediator.Send(addAssignmentFile);
            return Ok(assignment);
        }

        [HttpGet("file")]
        public async Task<IActionResult> DownloadFile(Guid attachmentId)
        {
            var result = await mediator.Send(new DownloadAssignmentFile() { AttachmentId = attachmentId });
            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpDelete("file/{attachmentId}")]
        public async Task<IActionResult> DeleteFile(Guid attachmentId)
        {
            await mediator.Send(new DeleteAssignmentFile() { AttachmentId = attachmentId });
            return Ok();
        }

        [HttpPost("RequiredFile")]
        public async Task<IActionResult> AddRequiredFile(AddRequiredFile addRequiredFile)
        {
            var file = await mediator.Send(addRequiredFile);
            return Ok(file);
        }
        [HttpDelete("RequiredFile/{requiredFileId}")]
        public async Task<IActionResult> DeleteRequiredFile(Guid requiredFileId)
        {
            await mediator.Send(new DeleteRequiredFile() { RequiredFileId = requiredFileId });
            return Ok();
        }
        [HttpPatch("RequiredFile")]
        public async Task<IActionResult> PatchRequiredFile(UpdateRequiredFile updateRequiredFile)
        {
            var result = await mediator.Send(updateRequiredFile);
            return Ok(result);
        }

    }
}

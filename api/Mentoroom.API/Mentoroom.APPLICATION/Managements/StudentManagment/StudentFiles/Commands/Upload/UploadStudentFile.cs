using MediatR;
using Mentoroom.APPLICATION.Models;
using Microsoft.AspNetCore.Http;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Commands.Upload
{
    public class UploadStudentFile : IRequest<StudentAssignmentModel>
    {
        public string StudentId { get; set; }
        public Guid AssignmentFileId { get; set; }

        public IFormFile File { get; set; }

    }
}

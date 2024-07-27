using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Commands.Delete
{
    public class DeleteStudentFile : IRequest
    {
        public string StudentId { get; set; }
        public Guid AssignmentFileId { get; set; }
    }
}

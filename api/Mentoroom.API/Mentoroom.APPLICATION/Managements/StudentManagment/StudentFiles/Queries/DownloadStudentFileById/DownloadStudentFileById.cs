using MediatR;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFileById
{
    public class DownloadStudentFileById : IRequest<Blob>
    {
        public string StudentId { get; set; }
        public Guid AssignmentFileId { get; set; }
    }
}

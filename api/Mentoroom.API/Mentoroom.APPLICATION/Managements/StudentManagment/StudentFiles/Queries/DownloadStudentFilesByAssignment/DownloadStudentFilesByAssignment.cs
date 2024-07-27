using MediatR;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFilesByAssignment
{
    public class DownloadStudentFilesByAssignment : IRequest<Blob>
    {
        public Guid AssignmentId { get; set; }
    }
}

using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFilesByCourse
{
    public class DownloadStudentFilesByCourse : IRequest<DOMAIN.Models.Blob>
    {
        public Guid CourseId { get; set; }
    }
}

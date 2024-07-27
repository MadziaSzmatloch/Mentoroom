using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentsToConfirm
{
    public class GetStudentsToConfirm : IRequest<List<GetStudentModel>>
    {
        public Guid CourseId { get; set; }
    }
}

using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentsFromCourse
{
    public class GetStudentsFromCourse : IRequest<List<GetStudentModel>>
    {
        public Guid CourseId { get; set; }
    }
}

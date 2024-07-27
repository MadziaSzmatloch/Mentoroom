using MediatR;

namespace Mentoroom.APPLICATION.Managements.Course.Queries.GetCourseByTeacher
{
    public class GetCourseByTeacher : IRequest<IEnumerable<CourseDto>>
    {
        public string TeacherId { get; set; } = string.Empty;
    }
}

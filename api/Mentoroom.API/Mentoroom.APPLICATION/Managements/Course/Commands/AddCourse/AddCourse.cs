using MediatR;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.AddCourse
{
    public class AddCourse : CoursePost, IRequest<CourseDto>
    {
    }
}

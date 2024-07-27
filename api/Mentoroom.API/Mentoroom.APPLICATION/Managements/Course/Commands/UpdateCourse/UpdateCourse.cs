using MediatR;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.UpdateCourse
{
    public class UpdateCourse : CoursePost, IRequest<CourseDto>
    {
        public Guid Id { get; set; }
    }
}

using MediatR;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.DeleteCourse
{
    public class DeleteCourse : IRequest
    {
        public Guid CourseId { get; set; }
    }
}

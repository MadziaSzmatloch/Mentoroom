using MediatR;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.CoAuthor.DeleteCoAuthorFromCourse
{
    public class DeleteCoAuthorFromCourse : IRequest
    {
        public Guid CourseId { get; set; }
        public string CoAuthorId { get; set; }
    }
}

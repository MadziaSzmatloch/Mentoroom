using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.AddCoAuthorToCourse
{
    public class AddCoAuthorToCourse : IRequest<UserModel>
    {
        public Guid CourseId { get; set; }
        public string CoAuthorId { get; set; }
    }
}

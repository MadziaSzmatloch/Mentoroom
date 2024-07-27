using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Lecturer.GetLecturers
{
    public class GetLecturesCommand : IRequest<List<UserModel>>
    {
    }
}

using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Users.Queries.GetLecturers
{
    public class GetLecturersQuerry : IRequest<List<UserModel>>
    {
    }
}

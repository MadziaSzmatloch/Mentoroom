using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Users.Queries.GetUsers
{
    public class GetUsersQuerry : IRequest<List<UserModel>>
    {
    }
}

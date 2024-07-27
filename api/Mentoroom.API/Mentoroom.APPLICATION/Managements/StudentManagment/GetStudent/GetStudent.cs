using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.GetAllStudents
{
    public class GetStudent : IRequest<IEnumerable<StudentModel>>
    {
    }
}

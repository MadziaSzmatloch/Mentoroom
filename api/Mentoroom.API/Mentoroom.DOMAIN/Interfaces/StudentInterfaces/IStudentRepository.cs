using Mentoroom.DOMAIN.Entities.StudentModels;

namespace Mentoroom.DOMAIN.Interfaces.StudentInterfaces
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(string studentId);
        Task RemoveStudentByUserId(string userId);
    }
}

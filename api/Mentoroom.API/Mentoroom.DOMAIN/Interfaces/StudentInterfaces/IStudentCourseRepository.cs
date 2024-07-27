using Mentoroom.DOMAIN.Entities.StudentModels;

namespace Mentoroom.DOMAIN.Interfaces.StudentInterfaces
{
    public interface IStudentCourseRepository
    {
        Task<StudentCourse> AddStudentCourse(StudentCourse studentCourse);
        Task<IEnumerable<StudentCourse>> GetStudentCoursesByStudentId(string studentId); //all
        Task<IEnumerable<StudentCourse>> GetStudentCoursesByCourseId(Guid courseId); //all
        Task<IEnumerable<StudentCourse>> GetAllStudentCourses();
        Task<StudentCourse> DeleteStudentCourse(StudentCourse studentCourse);
        Task<StudentCourse> ConfirmStudent(string studentId, Guid courseId);
        Task<StudentCourse> RemoveStudentCourse(string studentId, Guid courseId);
    }
}

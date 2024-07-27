using Mentoroom.DOMAIN.Entities.Shared;

namespace Mentoroom.DOMAIN.Interfaces.Shared
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(Course course);
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> DeleteCourseById(Course course);
        Task<Course> UpdateCourse(Course course);
        Task<Course> GetCourseById(Guid courseId);
        Task<IEnumerable<Course>> GetCoursesByTeacher(string userId);

    }
}

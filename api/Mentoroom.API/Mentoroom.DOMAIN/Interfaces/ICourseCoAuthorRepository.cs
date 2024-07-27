using Mentoroom.DOMAIN.Entities.LecturerModels;

namespace Mentoroom.DOMAIN.Interfaces
{
    public interface ICourseCoAuthorRepository
    {
        Task<CourseCoAuthor> AddCourseCoAuthor(string authorId, Guid courseId);
        Task<List<CourseCoAuthor>> GetCoAuthorsByCourseId(Guid courseId);
        Task<CourseCoAuthor> DeleteCourseCoAuthor(CourseCoAuthor courseCoAuthor);
        Task<List<CourseCoAuthor>> GetCoAuthors();

    }
}

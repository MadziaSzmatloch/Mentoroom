using Mentoroom.DOMAIN.Entities.Tags;

namespace Mentoroom.DOMAIN.Interfaces.Tags
{
    public interface ISemesterRepository
    {
        Task<Semester> AddSemester(Semester semester);
        Task<IEnumerable<Semester>> GetAllSemester();
        Task<Semester> GetSemesterById(Guid id);
        Task<IEnumerable<Semester>> GetSemestersByYear(Guid id);
    }
}

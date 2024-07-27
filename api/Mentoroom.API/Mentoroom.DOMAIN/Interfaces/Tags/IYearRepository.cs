using Mentoroom.DOMAIN.Entities.Tags;

namespace Mentoroom.DOMAIN.Interfaces.Tags
{
    public interface IYearRepository
    {
        Task<Year> AddYear(Year year);
        Task<IEnumerable<Year>> GetAllYear();
        Task<Year> GetYearById(Guid id);
        Task<IEnumerable<Year>> GetYearsByDegree(Guid id);
    }
}

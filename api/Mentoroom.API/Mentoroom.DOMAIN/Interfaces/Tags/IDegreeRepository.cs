using Mentoroom.DOMAIN.Entities.Tags;

namespace Mentoroom.DOMAIN.Interfaces.Tags
{
    public interface IDegreeRepository
    {
        Task<Degree> AddDegree(Degree degree);
        Task<IEnumerable<Degree>> GetAllDegree();
        Task<Degree> GetDegreeById(Guid id);
    }
}

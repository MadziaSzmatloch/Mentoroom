using Mentoroom.DOMAIN.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoroom.DOMAIN.Interfaces.Tags
{
    public interface IMajorRepository
    {
        Task<Major> AddMajor(Major major);
        Task<IEnumerable<Major>> GetAllMajors();
        Task<IEnumerable<Major>> GetMajorsByDepartment(Guid departmentId);
        Task<Major> GetMajorById(Guid id);
    }
}

using Mentoroom.DOMAIN.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoroom.DOMAIN.Interfaces.Tags
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartment(Department department);
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(Guid id);
    }
}

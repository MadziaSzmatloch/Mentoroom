using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.Tags
{
    public class Major
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Mentoroom.DOMAIN.Entities.Tags
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public List<Major> Majors { get; set; }
    }
}

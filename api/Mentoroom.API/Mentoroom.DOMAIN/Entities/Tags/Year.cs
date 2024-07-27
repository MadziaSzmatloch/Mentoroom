using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.Tags
{
    public class Year
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Degree))]
        public Guid DegreeId { get; set; }
        public Degree Degree { get; set; }

        public List<Semester> Semesters { get; set; }
    }
}

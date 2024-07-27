using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.Tags
{
    public class Semester
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Year))]
        public Guid YearId { get; set; }
        public Year Year { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Mentoroom.DOMAIN.Entities.Tags
{
    public class Degree
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Year> Years { get; set; }
    }
}

using DHCardHelper.Models.Entities.Relationships;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities
{
    public class Domain
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(3, 8)]
        [DisplayName("Color")]
        [Required]
        public string Color { get; set; }

        public ICollection<ClassToDomainRel> ClassToDomainRel { get; set; } = new List<ClassToDomainRel>();
    }
}

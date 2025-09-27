using DHCardHelper.Models.Entities.Relationships;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities
{
    public class CharacterClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ClassToDomainRel> ClassToDomainRel { get; set; } = new List<ClassToDomainRel>();
    }
}

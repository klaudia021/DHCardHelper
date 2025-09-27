using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHCardHelper.Models.Entities.Relationships
{
    [PrimaryKey(nameof(ClassId), nameof(DomainId))]
    public class ClassToDomainRel
    {
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public CharacterClass CharacterClass { get; set; }


        public int DomainId { get; set; }

        [ForeignKey("DomainId")]
        public Domain Domain { get; set; }

    }
}

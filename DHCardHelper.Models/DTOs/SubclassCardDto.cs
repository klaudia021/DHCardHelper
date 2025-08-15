using DHCardHelper.Models.Entities.Cards;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.DTOs
{
    public class SubclassCardDto : CardDto
    {
        [Required]
        public string MasteryType { get; set; }

        [Required]
        public int CharacterClassId { get; set; }
    }
}

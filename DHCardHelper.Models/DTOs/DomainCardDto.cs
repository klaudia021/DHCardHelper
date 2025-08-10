using DHCardHelper.Models.Entities.Cards;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.DTOs
{
    public class DomainCardDto : CardDto
    {
        [Required]
        public int DomainId { get; set; }


        [Required]
        public int TypeId { get; set; }


        [Range(0, 20)]
        [Required]
        public int Level { get; set; }


        [Range(0, 10)]
        [DisplayName("Recall Cost")]
        [Required]
        public int RecallCost { get; set; }
    }
}

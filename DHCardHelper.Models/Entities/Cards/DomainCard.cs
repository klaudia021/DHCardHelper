using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DHCardHelper.Models.Entities.Cards
{
    public class DomainCard : Card
    {
        [Range(0,20)]
        [Required]
        public int? Level { get; set; }

        [Range(0,10)]
        [DisplayName("Recall Cost")]
        [Required]
        public int? RecallCost { get; set; }
    }
}

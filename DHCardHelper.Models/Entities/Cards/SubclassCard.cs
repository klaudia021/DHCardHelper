using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities.Cards
{
    public class SubclassCard : Card
    {
        [Required]
        public string MasteryType { get; set; }
    }
}

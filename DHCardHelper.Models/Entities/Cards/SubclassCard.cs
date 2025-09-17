using DHCardHelper.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities.Cards
{
    public class SubclassCard : Card
    {
        [Required]
        public MasteryType MasteryType { get; set; }
    }
}

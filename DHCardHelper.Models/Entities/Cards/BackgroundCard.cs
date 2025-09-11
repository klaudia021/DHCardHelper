using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities.Cards
{
    public class BackgroundCard : Card
    {
        [MinLength(10)]
        [Required]
        public string Desciption { get; set; }
    }
}

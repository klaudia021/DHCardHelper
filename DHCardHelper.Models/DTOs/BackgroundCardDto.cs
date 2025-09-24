using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.DTOs
{
    public class BackgroundCardDto : CardDto
    {
        [MinLength(10)]
        [Required]
        public string Desciption { get; set; }

        [Required]
        public int BackgroundTypeId { get; set; }

        public BackgroundCardType BackgroundType { get; set; }
    }
}

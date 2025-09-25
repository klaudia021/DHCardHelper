using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [ValidateNever]
        public BackgroundCardType BackgroundType { get; set; }
    }
}

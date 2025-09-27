using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Shared;
using DHCardHelper.Models.Types;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.DTOs
{
    public class SubclassCardDto : CardDto
    {
        [Required]
        public MasteryType MasteryType { get; set; }

        [Required]
        public int CharacterClassId { get; set; }

        public CharacterClass CharacterClass { get; set; }

        [ValidateNever]
        public GradientColor SubclassHeaderColor { get; set; } 
    }
}

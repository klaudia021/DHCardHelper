using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Entities.Characters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHCardHelper.Models.DTOs.Character
{
    public class CardSheetDto
    {
        [Required]
        public int CharacterSheetId { get; set; }

        [ValidateNever]
        public CharacterSheetDto CharacterSheetDto { get; set; }


        [Required]
        public int CardId { get; set; }

        [ValidateNever]
        public CardDto CardDto { get; set; }


        [Required]
        public bool InLoadout { get; set; }

        [Required]
        public bool InLimit { get; set; }
    }
}

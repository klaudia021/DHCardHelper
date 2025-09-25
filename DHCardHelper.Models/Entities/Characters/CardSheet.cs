using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHCardHelper.Models.Entities.Characters
{
    public class CardSheet
    {
        public int Id { get; set; }


        [Required]
        public int CharacterSheetId { get; set; }

        [ForeignKey("CharacterSheetId")]
        [ValidateNever]
        public CharacterSheet CharacterSheet { get; set; }

        [Required]
        public int CardId { get; set; }

        [ForeignKey("CardId")]
        [Required]
        public Card Card { get; set; }

        [Required]
        public bool InLoadout { get; set; }

        [Required]
        public bool InLimit { get; set; }
    }
}

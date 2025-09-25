using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.DTOs
{
    public class DomainCardDto : CardDto
    {
        [Required]
        public int DomainId { get; set; }

        [ValidateNever]
        public Domain Domain { get; set; }


        [Required]
        public int TypeId { get; set; }

        [ValidateNever]
        public DomainCardType DomainCardType { get; set; }


        [Range(0, 20)]
        [Required]
        public int Level { get; set; }


        [Range(0, 10)]
        [DisplayName("Recall Cost")]
        [Required]
        public int RecallCost { get; set; }
    }
}

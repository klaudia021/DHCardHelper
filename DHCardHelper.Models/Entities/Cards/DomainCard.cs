using DHCardHelper.Models.Entities.AvailableTypes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DHCardHelper.Models.Entities.Cards
{
    public class DomainCard : Card
    {
        [Required]
        public int DomainId { get; set; }

        [ForeignKey("DomainId")]
        [ValidateNever]
        public Domain Domain { get; set; }

        [Required]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        [ValidateNever]
        public AvailableTypes.Type Type { get; set; }

        [Range(0,20)]
        [Required]
        public int Level { get; set; }

        [Range(0,10)]
        [DisplayName("Recall Cost")]
        [Required]
        public int RecallCost { get; set; }
    }
}

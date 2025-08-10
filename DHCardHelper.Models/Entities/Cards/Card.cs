using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHCardHelper.Models.Entities.Cards
{
    public abstract class Card
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [MinLength(10)]
        [Required]
        public string Feature { get; set; }

        public int? DomainId { get; set; }

        [ForeignKey("DomainId")]
        [ValidateNever]
        public Domain? Domain { get; set; }

        public int? TypeId { get; set; }

        [ForeignKey("TypeId")]
        [ValidateNever]
        public Entities.Type? Type { get; set; }

    }
}

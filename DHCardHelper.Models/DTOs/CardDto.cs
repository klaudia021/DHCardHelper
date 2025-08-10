using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities.Cards
{
    public abstract class CardDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [MinLength(10)]
        [Required]
        public string Feature { get; set; }
    }
}

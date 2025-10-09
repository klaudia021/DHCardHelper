using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities
{
    public class BackgroundCardType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

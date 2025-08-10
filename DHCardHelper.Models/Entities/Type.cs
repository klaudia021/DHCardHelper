using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities
{
    public class Type
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

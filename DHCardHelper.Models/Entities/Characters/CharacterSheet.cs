using DHCardHelper.Models.Entities.Users;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHCardHelper.Models.Entities.Characters
{
    public class CharacterSheet
    {
        public int Id { get; set; }


        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}

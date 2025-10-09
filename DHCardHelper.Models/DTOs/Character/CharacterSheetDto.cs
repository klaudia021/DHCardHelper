using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.DTOs.Character
{
    public class CharacterSheetDto
    {
        public int Id { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}

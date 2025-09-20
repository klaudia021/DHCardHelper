using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DHCardHelper.Models.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
    }
}

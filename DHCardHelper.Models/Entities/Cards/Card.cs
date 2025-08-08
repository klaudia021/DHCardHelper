using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

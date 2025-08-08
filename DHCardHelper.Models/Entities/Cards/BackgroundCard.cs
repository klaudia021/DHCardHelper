using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Models.Entities.Cards
{
    public class BackgroundCard : Card
    {
        [MinLength(10)]
        [Required]
        public string Desciption { get; set; }
    }
}

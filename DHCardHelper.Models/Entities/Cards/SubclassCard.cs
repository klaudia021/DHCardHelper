using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Models.Entities.Cards
{
    public class SubclassCard : Card
    {
        [Required]
        public string MasteryType { get; set; }
    }
}

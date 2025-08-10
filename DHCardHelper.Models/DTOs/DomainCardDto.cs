using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Models.DTOs
{
    public class DomainCardDto : Card
    {
        [Required]
        public int DomainId { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Range(0, 20)]
        [Required]
        public int Level { get; set; }

        [Range(0, 10)]
        [DisplayName("Recall Cost")]
        [Required]
        public int RecallCost { get; set; }
    }
}

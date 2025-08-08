using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Models.Entities
{
    public class Domain
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

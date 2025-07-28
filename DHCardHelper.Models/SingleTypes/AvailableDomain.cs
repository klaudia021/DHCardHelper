using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Models.Domains
{
    public class AvailableDomain
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

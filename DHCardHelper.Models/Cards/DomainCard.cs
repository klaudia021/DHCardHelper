using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCardHelper.Models.Cards
{
    public class DomainCard : Card
    {
        public string Domain { get; set; }
        public int Level { get; set; }
        public int RecallCost { get; set; }
    }
}

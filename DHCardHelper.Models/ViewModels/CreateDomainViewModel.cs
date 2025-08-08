using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Models.ViewModels
{
    public class CreateDomainViewModel
    {
        public DomainCard DomainCard { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableTypes { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableDomains { get; set; }
    }
}

using DHCardHelper.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Models.ViewModels
{
    public class UpsertDomainViewModel
    {
        public DomainCardDto DomainCardDto { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableTypes { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableDomains { get; set; }
    }
}

using DHCardHelper.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Models.ViewModels
{
    public class UpsertSubclassViewModel
    {
        public SubclassCardDto SubclassCardDto { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableClasses { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableMasteryTypes { get; set; }
    }
}

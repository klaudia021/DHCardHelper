using DHCardHelper.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Models.ViewModels
{
    public class UpsertBackgroundViewModel
    {
        public BackgroundCardDto BackgroundCardDto { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> HeritageTypes { get; set; }
    }
}

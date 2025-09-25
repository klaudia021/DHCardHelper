using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DHCardHelper.Models.ViewModels
{
    public class AddCardToSheetViewModel<T> where T : CardDto
    {
        public IEnumerable<T> CardList { get; set; }
        public IEnumerable<SelectListItem> CharacterSheetList { get; set; }
    }
}

using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DHCardHelper.Pages.Cards.Subclasses
{
    public class IndexModel : PageModel
    {
        public AddCardToSheetViewModel<SubclassCardDto> CardToSheetViewModel { get; set; } = new AddCardToSheetViewModel<SubclassCardDto>();

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;

        public IndexModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            await SetSubclassCards();
            await SetCharacterSheets();
        }

        private async Task SetCharacterSheets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId.IsNullOrEmpty())
            {
                TempData["Error"] = "User is not found. Try logging in again!";
                _logger.Error("User not found in Subclasses Index!");

                return;
            }

            var characterSheets = await _unitOfWork.CharacterSheetRepository.GetListWithFilterAsync(s => s.UserId == userId);
            CardToSheetViewModel.CharacterSheetList = characterSheets.Select((s) => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });
        }

        private async Task SetSubclassCards()
        {
            var subclassCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<SubclassCard>(s => s.CharacterClass);

            CardToSheetViewModel.CardList = subclassCards.Adapt<List<SubclassCardDto>>();
        }
    }
}

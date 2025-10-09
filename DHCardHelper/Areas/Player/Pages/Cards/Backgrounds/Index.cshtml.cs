using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.ViewModels;
using DHCardHelper.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DHCardHelper.Pages.Cards.Backgrounds
{
    public class IndexModel : PageModel
    {
        public AddCardToSheetViewModel<BackgroundCardDto> CardToSheetViewModel { get; set; } = new AddCardToSheetViewModel<BackgroundCardDto>();

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;

        public IndexModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            await SetBackgroundCards();
            await SetCharacterSheets();
        }

        private async Task SetCharacterSheets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId.IsNullOrEmpty())
            {
                TempData["Error"] = "User is not found. Try logging in again!";
                _logger.Error("User not found in Backgrounds Index!");

                return;
            }

            var characterSheets = await _unitOfWork.CharacterSheetRepository.GetListWithFilterAsync(s => s.UserId == userId);
            CardToSheetViewModel.CharacterSheetList = characterSheets.Select((s) => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });
        }

        private async Task SetBackgroundCards()
        {
            var subclassCards = await _unitOfWork.CardRepository.GetAllByTypeAsync<BackgroundCard>(b => b.BackgroundType);

            CardToSheetViewModel.CardList = subclassCards.Adapt<List<BackgroundCardDto>>();
        }
    }
}

using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Models.Shared;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHCardHelper.Areas.Player.Pages.Characters
{
    public class CharacterModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CharacterModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CardSheetDto> CardSheets { get; set; } = new List<CardSheetDto>();
        public string CharacterName { get; set; }


        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            await SetCharacterName(id);
            await SetCardSheets(id);
            await SetSubclassHeaderColor();

            return Page();
        }

        private async Task SetCardSheets(int? id)
        {
            var cardSheets = await _unitOfWork.CardSheetRepository.GetAllCharacterCards(id);

            CardSheets = cardSheets.Adapt<List<CardSheetDto>>();
        }

        private async Task SetCharacterName(int? id)
        {
            var character = await _unitOfWork.CharacterSheetRepository.GetFirstOrDefaultAsync(s => s.Id == id);

            CharacterName = character.Name;
        }

        private async Task SetSubclassHeaderColor()
        {
            var domainsToClass = await _unitOfWork.ClassToDomainRelRepository.GetDomainsForClass();

            foreach (var card in CardSheets)
            {
                if (card.CardDto is SubclassCardDto subclassCard)
                {
                    var characterClass = domainsToClass.FirstOrDefault(cl => cl.Id == subclassCard.CharacterClassId);

                    if (characterClass != null)
                    {
                        card.SubclassHeaderColor = new GradientColor
                        {
                            Start = characterClass.Domains?.ElementAtOrDefault(0)?.Color ?? "#ffffff",
                            End = characterClass.Domains?.ElementAtOrDefault(1)?.Color ?? "#ffffff"
                        };
                    }
                }
            }
        }
    }
}

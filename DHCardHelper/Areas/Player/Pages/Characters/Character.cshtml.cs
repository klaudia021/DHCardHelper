using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.DTOs;
using DHCardHelper.Models.DTOs.Character;
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


    }
}

using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Models.Entities.Characters;
using DHCardHelper.Utilities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DHCardHelper.Areas.Player.Pages.Characters.Cards
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        public AddModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(int? characterSheetId, int? cardId)
        {
            var cardEntity = await _unitOfWork.CardRepository.GetFirstOrDefaultAsync(c => c.Id == cardId);
            if (cardEntity == null)
                return new JsonResult(new { success = false, message = "Card not found!" });

            var sheetEntity = await _unitOfWork.CharacterSheetRepository.GetFirstOrDefaultAsync(s => s.Id == characterSheetId);
            if (sheetEntity == null)
                return new JsonResult(new { success = false, message = "Character Sheet not found!" });

            var cardAlreadyAdded = await _unitOfWork.CardSheetRepository.AnyAsync(c =>
                c.CardId == cardId &&
                c.CharacterSheetId == characterSheetId
            );

            if (cardAlreadyAdded)
                return new JsonResult(new { success = false, message = "Card already added to character!" });

            var cardSheetEntity = new CardSheet
            {
                CharacterSheet = sheetEntity,
                Card = cardEntity,
                InLimit = false,
                InLoadout = false
            };

            await _unitOfWork.CardSheetRepository.AddAsync(cardSheetEntity);

            try
            {
                await _unitOfWork.SaveAsync();

                return new JsonResult(new { success = true, message = "Card added successfully!" });
            }
            catch (DbUpdateException ex)
            {
                _logger.Error(ex.Message);

                return new JsonResult(new { success = false, message = "Unable to save data. Please check the data." });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new JsonResult(new { success = false, message = "There was a database error. Please try again." });
            }
        }
    }
}

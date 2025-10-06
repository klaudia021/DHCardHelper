using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DHCardHelper.Areas.Player.Pages.Characters.Cards
{
    [ValidateAntiForgeryToken]
    [Area("Player")]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyLogger _logger;
        public DeleteModel(IUnitOfWork unitOfWork, IMyLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> OnDeleteAsync(int? characterSheetId, int? cardId)
        {
            if (characterSheetId == null)
                return new JsonResult(new { success = false, message = "Character not found!" });

            if (cardId == null)
                return new JsonResult(new { success = false, message = "Card not found!" });

            var cardSheetEntity = await _unitOfWork.CardSheetRepository.GetFirstOrDefaultAsync(s => s.CardId == cardId && s.CharacterSheetId == characterSheetId);
            if (cardSheetEntity == null)
                return new JsonResult(new { success = false, message = "Entity not found by these parameters!" });

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var characterOwnerUser = await _unitOfWork.CharacterSheetRepository.GetFirstOrDefaultAsync(c => c.Id == characterSheetId);

            if (characterOwnerUser == null || (currentUserId != characterOwnerUser.UserId))
                return new JsonResult(new { success = false, message = "You do not have permission to modify this character!" });


            _unitOfWork.CardSheetRepository.Remove(cardSheetEntity);

            try
            {
                await _unitOfWork.SaveAsync();

                return new JsonResult(new { success = true, message = "Card removed successfully!" });
            }
            catch (DbUpdateException ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult(new { success = false, message = "There was an error while saving!" });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult(new { success = false, message = "Something went wrong!" });
            }
        }
    }
}

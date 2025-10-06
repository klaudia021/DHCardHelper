using DHCardHelper.Data.Repository.IRepository;
using DHCardHelper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DHCardHelper.Areas.Player.Pages.Characters
{
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
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Character not found!";
                return Redirect("/Player/Characters");
            }

            var characterSheetEntity = await _unitOfWork.CharacterSheetRepository.GetFirstOrDefaultAsync(s => s.Id == id);
            if (characterSheetEntity == null)
            {
                TempData["Error"] = "Entity not found by these parameters!";
                return Redirect("/Player/Characters");
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != characterSheetEntity.UserId)
            {
                TempData["Error"] = "You do not have permission to delete this character!";
                return Redirect("/Player/Characters");
            }

            _unitOfWork.CharacterSheetRepository.Remove(characterSheetEntity);

            try
            {
                await _unitOfWork.SaveAsync();

                TempData["Success"] = "Character deleted successfully!";
            }
            catch (DbUpdateException ex)
            {
                _logger.Error(ex.Message);
                TempData["Error"] = "There was an error while saving!";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                TempData["Error"] = "Something went wrong!";
            }

            return Redirect("/Player/Characters");
        }
    }
}
